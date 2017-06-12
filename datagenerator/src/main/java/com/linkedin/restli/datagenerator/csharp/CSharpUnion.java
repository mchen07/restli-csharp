package com.linkedin.restli.datagenerator.csharp;


import com.linkedin.pegasus.generator.CodeUtil;
import com.linkedin.pegasus.generator.spec.ClassTemplateSpec;
import com.linkedin.pegasus.generator.spec.UnionTemplateSpec;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.TreeSet;
import java.util.regex.Pattern;
import java.util.stream.Collectors;


/**
 * @author Evan Williams
 */
public class CSharpUnion extends CSharpComplexType {
  public class Member {
    public String key;
    public CSharpType type;

    public Member(String key, CSharpType type) {
      this.key = key;
      this.type = type;
    }
  }

  private final UnionTemplateSpec _union;
  private final CSharpDataTemplateGenerator _dataTemplateGenerator;
  private Map<String, Member> _members;

  public CSharpUnion(ClassTemplateSpec spec, CSharpType enclosingType, CSharpDataTemplateGenerator dataTemplateGenerator) {
    super(spec, enclosingType);
    _dataTemplateGenerator = dataTemplateGenerator;
    _union = (UnionTemplateSpec) spec;
  }

  @Override
  public String getName(NameModifier modifier) {
    switch (modifier) {
      case IN_BUILDER:
        if (_enclosingType != null) {
          return _enclosingType.getName(NameModifier.NONE) + "." + getName(NameModifier.NONE);
        }
        // else use default case
      default:
        return super.getName(modifier);
    }
  }

  public String getNameInBuilder() {
    if (_enclosingType == null) {
      return getName();
    } else {
      return _enclosingType.getName() + "." + getName();
    }
  }

  /**
   * Gets the complex types referenced by union members.
   * Note that skip-deprecated for union members is not supported (unlike for record fields) due to the backwards
   * incompatible nature of such a change.
   * @param skipDeprecatedArgs the set of namespaces included for skipping deprecated records and fields
   * @return the set of complex types referenced by union members.
   */
  @Override
  public Set<CSharpComplexType> getReferencedComplexTypes(List<Pattern> skipDeprecatedArgs) {
    return getMembers().values().stream()
        .filter(member -> member.type != this && member.type instanceof CSharpComplexType)
        .map(member -> (CSharpComplexType) member.type)
        .collect(Collectors.toCollection(() -> new TreeSet<>(nameComparator)));
  }

  public Map<String, Member> getMembers() {
    if (_members == null) {
      _members = new HashMap<>();

      for (UnionTemplateSpec.Member specMember : _union.getMembers()) {
        final String memberName = CodeUtil.getUnionMemberName(specMember.getSchema());
        final String memberKey = specMember.getSchema().getUnionMemberKey();
        final CSharpType memberType = _dataTemplateGenerator.generate(
            specMember.getCustomInfo() != null ? CSharpUtil.templateSpecFromCustomInfo(specMember.getCustomInfo()) : specMember.getClassTemplateSpec(),
            specMember.getDataClass());
        final Member member = new Member(memberKey, memberType);
        _members.put(memberName, member);
      }
    }

    return _members;
  }
}
