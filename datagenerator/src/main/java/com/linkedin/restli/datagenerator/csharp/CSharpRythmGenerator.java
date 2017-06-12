package com.linkedin.restli.datagenerator.csharp;

import com.linkedin.pegasus.generator.DataSchemaParser;
import com.linkedin.pegasus.generator.TemplateSpecGenerator;
import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.Writer;
import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.Set;
import org.rythmengine.Rythm;
import org.apache.commons.cli.CommandLine;
import org.apache.commons.cli.CommandLineParser;
import org.apache.commons.cli.DefaultParser;
import org.apache.commons.cli.HelpFormatter;
import org.apache.commons.cli.Option;
import org.apache.commons.cli.Options;
import org.apache.commons.cli.ParseException;
import org.rythmengine.conf.RythmConfigurationKey;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;


/**
 * Class to generate C# files from Rythm files and PDSC templates.
 *
 * @author Evan Williams
 */
public class CSharpRythmGenerator {
  public static final String TEMPLATE_PATH_ROOT = "rythm";
  private static final String C_SHARP_FILE_EXTENSION = ".cs";
  private static final Options _options = new Options();
  private static final Logger LOG = LoggerFactory.getLogger(CSharpRythmGenerator.class);
  static {
    _options.addOption(Option.builder().longOpt("resolverPath").hasArg().build());
    _options.addOption(Option.builder().longOpt("output").required().hasArg().build());
    _options.addOption(Option.builder().longOpt("source").required().hasArgs().build());
  }

  private final CommandLine _cl;

  // Mapping class types to path of Rythm template file
  private static Map<Class, String> templateMap = new HashMap<Class, String>();
  static {
    templateMap.put(CSharpEnum.class, TEMPLATE_PATH_ROOT + File.separator + "enum.rythm");
    templateMap.put(CSharpRecord.class, TEMPLATE_PATH_ROOT + File.separator + "record.rythm");
  }

  public static void main(String[] args) throws IOException {
    CommandLine cl = null;
    try
    {
      final CommandLineParser parser = new DefaultParser();
      cl = parser.parse(_options, args);
    }
    catch (ParseException e)
    {
      LOG.error("Invalid arguments: " + e.getMessage());
      reportInvalidArguments();
    }

    final CSharpRythmGenerator generator = new CSharpRythmGenerator(cl);
    generator.setupRythmEngine();
    generator.generate();

    Rythm.shutdown();
  }

  public CSharpRythmGenerator(CommandLine cl) {
    _cl = cl;
  }

  public void setupRythmEngine() {
    final Map<String, Object> config = new HashMap<>();
    ResourceStreamResourceLoader loader = new ResourceStreamResourceLoader();
    config.put(RythmConfigurationKey.CODEGEN_COMPACT_ENABLED.getKey(), false);
    config.put(RythmConfigurationKey.RESOURCE_LOADER_IMPLS.getKey(), loader);
    Rythm.init(config);
    loader.setEngine(Rythm.engine());
    Rythm.engine().registerTransformer(CSharpRythmTransformer.class);
  }

  public void generate() throws IOException {
    final File outputDirectory = new File(_cl.getOptionValue("output"));

    final DataSchemaParser schemaParser = new DataSchemaParser(_cl.getOptionValue("resolverPath"));
    final DataSchemaParser.ParseResult parseResult = schemaParser.parseSources(_cl.getOptionValues("source"));

    final TemplateSpecGenerator specGenerator = new TemplateSpecGenerator(schemaParser.getSchemaResolver());
    generateTemplateSpecs(specGenerator, parseResult);

    final CSharpDataTemplateGenerator dataTemplateGenerator = new CSharpDataTemplateGenerator();

    final int renderedCount = generateResultFiles(dataTemplateGenerator, specGenerator.getGeneratedSpecs(), outputDirectory);

    LOG.info("Generated files for " + renderedCount + " models");
  }

  private void generateTemplateSpecs(TemplateSpecGenerator specGenerator, DataSchemaParser.ParseResult parseResult) {
    parseResult.getSchemaAndLocations().forEach(specGenerator::generate);
  }

  private int generateResultFiles(CSharpDataTemplateGenerator dataTemplateGenerator,
                                  Collection<ClassTemplateSpec> specs,
                                  File outputDirectory)
      throws IOException {

    specs.forEach(dataTemplateGenerator::generate);

    int renderedCount = 0;
    while (true) {
      final Set<CSharpType> unprocessedTypes = dataTemplateGenerator.getUnprocessedTypes();
      if (unprocessedTypes.isEmpty()) {
        break;
      }

      for (CSharpType type : unprocessedTypes) {
        final ClassTemplateSpec spec = type.getSpec();

        if (type instanceof CSharpComplexType && !(type instanceof CSharpUnion)) {
          final CSharpComplexType templateType = (CSharpComplexType) type;
          try {
            final String modelsTemplate = getTemplateName(templateType);
            final String renderResult = renderToString(modelsTemplate, templateType);
            if (renderResult.isEmpty()) {
              throw new IOException("Rythm template does not exist at '" + modelsTemplate + "'");
            } else {
              outputDirectory.mkdirs();
              writeToFile(new File(outputDirectory, templateType.getName() + C_SHARP_FILE_EXTENSION), renderResult);
              renderedCount++;
            }
          } catch (IOException e) {
            LOG.error("Failed to generate file for " + templateType.getName());
            throw e;
          }
        }
      }
    }

    return renderedCount;
  }

  public String getTemplateName(CSharpComplexType type) {
    String path = templateMap.get(type.getClass());
    if (path != null) {
      return path;
    } else {
      throw new IllegalArgumentException("Type does not have associated template: " + type.getClass().getName());
    }
  }

  private String renderToString(String templatePath, CSharpType type)
      throws IOException {
    final String result = Rythm.renderIfTemplateExists(templatePath, type, this, Rythm.engine());
    if (result.isEmpty()) {
      throw new IOException("The template does not exist: " + templatePath);
    } else {
      return result;
    }
  }

  private static void writeToFile(File file, String content)
      throws IOException {
    Writer fileWriter = null;
    try {
      fileWriter = new BufferedWriter(new FileWriter(file));
      fileWriter.write(content);
    } finally {
      if (fileWriter != null) {
        fileWriter.close();
      }
    }
  }

  private static void reportInvalidArguments()
  {
    final HelpFormatter formatter = new HelpFormatter();
    formatter.printHelp(CSharpRythmGenerator.class.getName(), _options, true);
    System.exit(1);
  }

}
