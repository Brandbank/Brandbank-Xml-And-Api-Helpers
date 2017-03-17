using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brandbank.Xml.MessageHelpers
{
    public static class LanguageTypeReaderExtensions
    {
        public static string GetDescription(
            this LanguageType languageType) => 
            languageType.Description ?? string.Empty;

        public static string GetCategoryCodes(
            this LanguageType languageType,
            string schemeName) => string.Join("", languageType
            .Categorisations
            .FirstOrNewIfNull(c => c.Scheme.Equals(schemeName, StringComparison.CurrentCultureIgnoreCase))
            .Level
            .NewIfNull()
            .Select(l => l.Code));

        #region Statements
        public static bool StatementExists(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetStatementItems(itemTypeId)
            .Any(lwct => lwct.Id.Equals(nameTypeId));

        public static string GetStatementValue(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetStatementItems(itemTypeId)
            .FirstOrNewIfNull(lwct => lwct.Id.Equals(nameTypeId))
            .Value
            .NewIfNull();
        
        public static IEnumerable<LookupWithCodeType> GetStatementItems(
            this LanguageType languageType,
            string itemTypeId
            ) => languageType
            .GetBaseTypeItems(itg => itg.Statements)
            .FirstOrNewIfNull(st => st.Id.Equals(itemTypeId))
            .Statement
            .NewIfNull();

        #endregion

        #region NameLookup
        public static IEnumerable<NameValueType> GetNameLookupItems(
            this LanguageType languageType,
            string id) => languageType
            .GetBaseTypeItems(itg => itg.NameLookups)
            .FirstOrNewIfNull(nl => nl.Id.Equals(id))
            .NameValue
            .NewIfNull();

        public static NameValueType GetNameLookupItem(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId) => languageType
            .GetNameLookupItems(itemTypeId)
            .FirstOrNewIfNull(nv => nv.Name.Id.Equals(nameTypeId));

        public static LookupType GetNameLookupName(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId) => languageType
            .GetNameLookupItem(itemTypeId, nameTypeId)
            .Name
            .NewIfNull();

        public static string GetNameLookupNameValue(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId) => languageType
            .GetNameLookupName(itemTypeId, nameTypeId)
            .Value
            .NewIfNull();

        public static string GetNameLookupNameId(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId) => languageType
            .GetNameLookupName(itemTypeId, nameTypeId)
            .Id
            .NewIfNull();

        public static LookupWithCodeType GetNameLookupValue(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId) => languageType
            .GetNameLookupItem(itemTypeId, nameTypeId)
            .Value
            .NewIfNull();

        public static string GetNameLookupValueId(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId) => languageType
            .GetNameLookupValue(itemTypeId, nameTypeId)
            .Id
            .NewIfNull();

        public static string GetNameLookupValueCode(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId) => languageType
            .GetNameLookupValue(itemTypeId, nameTypeId)
            .Code
            .NewIfNull();

        public static string GetNameLookupValueValue(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId) => languageType
            .GetNameLookupValue(itemTypeId, nameTypeId)
            .Value
            .NewIfNull();

        #endregion

        #region longtext
        public static IEnumerable<string> GetLongTextItems(
            this LanguageType languageType,
            string id
            ) => languageType
            .GetBaseTypeItems(itg => itg.LongTextItems)
            .FirstOrNewIfNull(lt => lt.Id.Equals(id))
            .Text
            .NewIfNull()
            .Select(tt => tt.Value);

        public static string GetLongTextItems(
            this LanguageType languageType,
            string id,
            string delimiter
            ) => string.Join(delimiter, languageType.GetLongTextItems(id));
        #endregion

        #region Memos
        public static MemoType GetMemoItem(
            this LanguageType languageType,
            string itemTypeId
            ) => languageType
            .GetBaseTypeItems(itg => itg.Memo)
            .FirstOrNewIfNull(m => m.Id.Equals(itemTypeId));

        public static string GetMemo(
            this LanguageType languageType,
            string itemTypeId
            ) => languageType
            .GetMemoItem(itemTypeId)
            .Value
            .NewIfNull();
        #endregion

        #region NameText
        public static float GetNameTextTextAsFloat(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameTextText(itemTypeId, nameTypeId)
            .ConvertToFloat();

        public static string GetNameTextText(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameTextItem(itemTypeId, nameTypeId)
            .Text
            .NewIfNull()
            .Value
            .NewIfNull();

        public static string GetNameTextName(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameTextItem(itemTypeId, nameTypeId)
            .Name
            .NewIfNull()
            .Value
            .NewIfNull();

        public static string GetNameTextCode(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameTextItem(itemTypeId, nameTypeId)
            .Name
            .NewIfNull()
            .Code
            .NewIfNull();

        public static NameTextType GetNameTextItem(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameTextItems(itemTypeId)
            .NameText
            .FirstOrNewIfNull(nt => nt.Name.Id.Equals(nameTypeId));

        public static NameTextItemsType GetNameTextItems(
            this LanguageType languageType,
            string id
            ) => languageType
            .GetBaseTypeItems(itg => itg.NameTextItems)
            .FirstOrNewIfNull(m => m.Id.Equals(id));
        #endregion

        #region name text lookup
        public static IEnumerable<NameValueTextType> GetNameLookupTextItems(
            this LanguageType languageType,
            string itemTypeId
            ) => languageType
            .GetBaseTypeItems(itg => itg.NameTextLookups)
            .FirstOrNewIfNull(m => m.Id.Equals(itemTypeId))
            .NameValueText
            .NewIfNull();

        public static NameValueTextType GetNameLookupTextItem(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameLookupTextItems(itemTypeId)
            .FirstOrNewIfNull(ntli => ntli.Name.Id.Equals(nameTypeId));

        public static LookupType GetNameLookupTextName(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameLookupTextItem(itemTypeId, nameTypeId)
            .Name
            .NewIfNull();

        public static string GetNameLookupTextNameValue(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameLookupTextName(itemTypeId, nameTypeId)
            .Value
            .NewIfNull();

        public static LookupWithCodeType GetNameLookupTextValue(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameLookupTextItem(itemTypeId, nameTypeId)
            .Value
            .NewIfNull();
        public static string GetNameLookupTextValueId(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameLookupTextValue(itemTypeId, nameTypeId)
            .Id
            .NewIfNull();

        public static string GetNameLookupTextValueCode(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameLookupTextValue(itemTypeId, nameTypeId)
            .Code
            .NewIfNull();

        public static string GetNameLookupTextValueValue(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameLookupTextValue(itemTypeId, nameTypeId)
            .Value
            .NewIfNull();

        public static string GetNameLookupTextText(
            this LanguageType languageType,
            string itemTypeId,
            string nameTypeId
            ) => languageType
            .GetNameLookupTextItem(itemTypeId, nameTypeId)
            .Text
            .NewIfNull()
            .Value
            .NewIfNull();
        #endregion

        public static string GetStructuredNutritionHeading(
            this LanguageType languageType,
            string id,
            string groupDefinitionId) => languageType
            .GetStructuredNutritionValueGroupDefinition(id, groupDefinitionId)
            .Name
            .NewIfNull();
        
        public static decimal GetStructuredNutritionTotal(
            this LanguageType languageType,
            string id,
            string groupDefinitionId) => languageType
            .GetStructuredNutritionAmountTotal(id, groupDefinitionId)
            .Value;

        public static string GetStructuredNutritionUnitName(
            this LanguageType languageType,
            string id,
            string groupDefinitionId) => languageType
            .GetStructuredNutritionAmountTotal(id, groupDefinitionId)
            .UnitName
            .NewIfNull();

        public static string GetStructuredNutritionUnitAbbriviation(
            this LanguageType languageType,
            string id,
            string groupDefinitionId) => languageType
            .GetStructuredNutritionAmountTotal(id, groupDefinitionId)
            .UnitAbbreviation
            .NewIfNull();

        public static string GetStructuredNutritionReferenceIntakeHeaderCol1(
            this LanguageType languageType,
            string id,
            string groupDefinitionId) => languageType
            .GetStructuredNutritionValueGroupDefinition(id, groupDefinitionId)
            .ReferenceIntakeHeader
            .NewIfNull()
            .ElementAtOrDefault(0)
            .NewIfNull();

        public static string GetStructuredNutritionReferenceIntakeHeaderCol2(
            this LanguageType languageType,
            string id,
            string groupDefinitionId) => languageType
            .GetStructuredNutritionValueGroupDefinition(id, groupDefinitionId)
            .ReferenceIntakeHeader
            .NewIfNull()
            .ElementAtOrDefault(1)
            .NewIfNull();

        public static decimal GetStructuredNutritionValue(
            this LanguageType languageType,
            string id,
            string groupDefinitionId,
            string nutrientId) => languageType
            .GetStructuredNutritionValueGroup(id, groupDefinitionId, nutrientId)
            .Amount
            .NewIfNull()
            .Value;

        public static string GetStructuredNutritionValueUnitAbbreviation(
            this LanguageType languageType,
            string id,
            string nutrientId) => languageType
            .GetStructuredNutritionValueUnit(id, nutrientId)
            .Abbreviation
            .NewIfNull();

        public static string GetStructuredNutritionValueUnitName(
            this LanguageType languageType,
            string id,
            string nutrientId) => languageType
            .GetStructuredNutritionValueUnit(id, nutrientId)
            .Name
            .NewIfNull();

        public static decimal GetStructuredNutritionReferenceIntakeValueCol1(
            this LanguageType languageType,
            string id,
            string groupDefinitionId,
            string nutrientId) => languageType
            .GetStructuredNutritionValueGroup(id, groupDefinitionId, nutrientId)
            .ReferenceIntake
            .NewIfNull()
            .ElementAtOrDefault(0)
            .NewIfNull()
            .Value;

        public static decimal GetStructuredNutritionReferenceIntakeValueCol2(
            this LanguageType languageType,
            string id,
            string groupDefinitionId,
            string nutrientId) => languageType
            .GetStructuredNutritionValueGroup(id, groupDefinitionId, nutrientId)
            .ReferenceIntake
            .NewIfNull()
            .ElementAtOrDefault(1)
            .NewIfNull()
            .Value;

        public static AmountTotalType GetStructuredNutritionAmountTotal(
            this LanguageType languageType,
            string id,
            string groupDefinitionId) => languageType
            .GetStructuredNutritionValueGroupDefinition(id, groupDefinitionId)
            .AmountTotal
            .NewIfNull();

        public static ValueGroupType GetStructuredNutritionValueGroup(
            this LanguageType languageType,
            string id,
            string groupDefinitionId,
            string nutrientId) => languageType
            .GetStructuredNutritionNutrient(id, nutrientId)
            .ValueGroup
            .FirstOrNewIfNull(vg => vg.Id.Equals(groupDefinitionId));

        public static StructuredNutritionUnitType GetStructuredNutritionValueUnit(
            this LanguageType languageType,
            string id,
            string nutrientId) => languageType
            .GetStructuredNutritionNutrient(id, nutrientId)
            .Unit
            .NewIfNull();

        public static IEnumerable<StructuredNutrientType> GetStructuredNutritionNutrients(
            this LanguageType languageType,
            string id) => languageType
            .GetStructuredNutrition(id)
            .Nutrients
            .NewIfNull();

        public static StructuredNutrientType GetStructuredNutritionNutrient(
            this LanguageType languageType,
            string id,
            string nutrientId) => languageType
            .GetStructuredNutritionNutrients(id)
            .FirstOrNewIfNull(n => n.Id.Equals(nutrientId));

        public static ValueGroupDefinitionType GetStructuredNutritionValueGroupDefinition(
            this LanguageType languageType,
            string id,
            string groupDefinitionId) => languageType
            .GetStructuredNutrition(id)
            .ValueGroupDefinitions
            .FirstOrNewIfNull(vgd => vgd.Id.Equals(groupDefinitionId));

        public static StructuredNutritionType GetStructuredNutrition(
            this LanguageType languageType,
            string id) => languageType
            .GetBaseTypeItems(itg => itg.StructuredNutrition)
            .FirstOrNewIfNull(sn => sn.Id.Equals(id));

        public static IEnumerable<T> GetBaseTypeItems<T>(
            this LanguageType languageType,
            Func<ItemTypeGroupType, IEnumerable<T>> fn
            ) where T : class => languageType
            .ItemTypeGroup
            .SelectManyOrDefault(fn);
    }
}
