package com.linkedin.restli.datagenerator.csharp;

import com.linkedin.data.schema.DataSchema;
import com.linkedin.data.schema.DataSchemaLocation;
import com.linkedin.data.schema.EnumDataSchema;
import com.linkedin.data.schema.RecordDataSchema;
import com.linkedin.pegasus.generator.DataSchemaParser;
import com.linkedin.pegasus.generator.TemplateSpecGenerator;
import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
import com.linkedin.pegasus.generator.spec.EnumTemplateSpec;
import com.linkedin.pegasus.generator.spec.RecordTemplateSpec;
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
 * Class to generate C# files from the Rythm files and PDSC templates.
 *
 * @author Evan Williams
 */
public class CSharpRythmGenerator {
  public static final String TEMPLATE_PATH_ROOT = "rythm";
  private static final Options _options = new Options();
  private static final Logger LOG = LoggerFactory.getLogger(CSharpRythmGenerator.class);
  static {
    _options.addOption(Option.builder().longOpt("resolverPath").hasArg().build());
    _options.addOption(Option.builder().longOpt("output").required().hasArg().build());
    _options.addOption(Option.builder().longOpt("source").required().hasArgs().build());
  }

  protected final CommandLine _cl;

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
    //Rythm.engine().registerTransformer(ObjCRythmTransformer.class);
  }

  public void generate() throws IOException {
    final File outputDirectory = new File(_cl.getOptionValue("output"));

    final DataSchemaParser schemaParser = new DataSchemaParser(_cl.getOptionValue("resolverPath"));
    final DataSchemaParser.ParseResult parseResult = schemaParser.parseSources(_cl.getOptionValues("source"));

    final TemplateSpecGenerator specGenerator = new TemplateSpecGenerator(schemaParser.getSchemaResolver());
    generateTemplateSpecs(specGenerator, parseResult);


    for (ClassTemplateSpec spec : specGenerator.getGeneratedSpecs()) {
      System.out.println(spec.getSchema().toString()); //TODO DEBUG
    }

    //TODO TEST

    generateResultFile(specGenerator.getGeneratedSpecs(), outputDirectory);
  }

  protected void generateTemplateSpecs(TemplateSpecGenerator specGenerator, DataSchemaParser.ParseResult parseResult) {
    parseResult.getSchemaAndLocations().forEach(specGenerator::generate);
  }

  protected void generateResultFile(Collection<ClassTemplateSpec> specs, File outputDirectory) {

    for (ClassTemplateSpec spec : specs) {

      if (spec instanceof EnumTemplateSpec) {
        EnumDataSchema schema = (EnumDataSchema) spec.getSchema();
        Map<String, Object> params = new HashMap<String, Object>();
        params.put("className", schema.getName());
        params.put("symbols", schema.getSymbols());
        params.put("symbolDocs", schema.getSymbolDocs());
        try {
          final String modelsTemplate = TEMPLATE_PATH_ROOT + File.separator + "enum.rythm";
          final String renderResult = Rythm.renderIfTemplateExists(modelsTemplate, params);
          if (renderResult.isEmpty()) {
            throw new IOException("The enum template does not exist at '" + modelsTemplate + "'");
          } else {
            outputDirectory.mkdirs();
            writeToFile(new File(outputDirectory, spec.getClassName() + ".cs"), renderResult);
          }
        } catch (IOException e) {
          throw new RuntimeException(e);
        }
      }

      else if (spec instanceof RecordTemplateSpec) {
        RecordDataSchema schema = (RecordDataSchema) spec.getSchema();
        Map<String, Object> params = new HashMap<String, Object>();
        params.put("className", schema.getName());
        params.put("fields", schema.getFields());
        try {
          final String modelsTemplate = TEMPLATE_PATH_ROOT + File.separator + "record.rythm";
          final String renderResult = Rythm.renderIfTemplateExists(modelsTemplate, params);
          if (renderResult.isEmpty()) {
            throw new IOException("The record template does not exist at '" + modelsTemplate + "'");
          } else {
            outputDirectory.mkdirs();
            writeToFile(new File(outputDirectory, spec.getClassName() + ".cs"), renderResult);
          }
        } catch (IOException e) {
          throw new RuntimeException(e);
        }
      }
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
