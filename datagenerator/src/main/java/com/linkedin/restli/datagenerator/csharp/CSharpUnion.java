/*
   Copyright (c) 2017 LinkedIn Corp.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

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
      case BUILDER_NULLABLE:
      case BUILDER:
        if (_enclosingType != null) {
          return _enclosingType.getName(NameModifier.IMMUTABLE) + "." + getName(NameModifier.IMMUTABLE);
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
