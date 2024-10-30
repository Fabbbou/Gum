﻿using CodeOutputPlugin.Models;
using Gum.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOutputPlugin.Manager
{
    public class CustomCodeGenerator
    {
        public static string GetCustomCodeForElement(ElementSave element, CodeOutputElementSettings elementSettings, CodeOutputProjectSettings projectSettings)
        {

            var stringBuilder = new StringBuilder();
            int tabCount = 0;

            #region Using Statements

            if (!string.IsNullOrWhiteSpace(projectSettings?.CommonUsingStatements))
            {
                stringBuilder.AppendLine(projectSettings.CommonUsingStatements);
            }

            if (!string.IsNullOrEmpty(elementSettings?.UsingStatements))
            {
                stringBuilder.AppendLine(elementSettings.UsingStatements);
            }
            #endregion

            #region Namespace Header/Opening {

            string namespaceName = CodeGenerator.GetElementNamespace(element, elementSettings, projectSettings);

            if (!string.IsNullOrEmpty(namespaceName))
            {
                stringBuilder.AppendLine(ToTabs(tabCount) + $"namespace {namespaceName}");
                stringBuilder.AppendLine(ToTabs(tabCount) + "{");
                tabCount++;
            }

            #endregion

            #region Class Header/Opening {

            // todo - this needs work! It's just placeholder so I can finish the rest of this method to get something with the right # of brackets

            string classHeader = GetClassHeader(element, projectSettings);

            stringBuilder.AppendLine(ToTabs(tabCount) + classHeader);
            stringBuilder.AppendLine(ToTabs(tabCount) + "{");
            tabCount++;
            #endregion

            stringBuilder.AppendLine(ToTabs(tabCount) + "partial void CustomInitialize()");
            stringBuilder.AppendLine(ToTabs(tabCount) + "{");
            stringBuilder.AppendLine(ToTabs(tabCount));
            stringBuilder.AppendLine(ToTabs(tabCount) + "}");

            #region Class Closing }
            tabCount--;
            stringBuilder.AppendLine(ToTabs(tabCount) + "}");
            #endregion

            if (!string.IsNullOrEmpty(namespaceName))
            {
                tabCount--;
                stringBuilder.AppendLine(ToTabs(tabCount) + "}");
            }

            return stringBuilder.ToString();
        }

        public static string GetClassHeader(ElementSave element, CodeOutputProjectSettings projectSettings)
        {
            var visualApi = CodeGenerator.GetVisualApiForElement(element);
            string inheritance = CodeGenerator.GetInheritance(element, projectSettings);

            var classHeader = $"partial class {CodeGenerator.GetClassNameForType(element.Name, visualApi)} : {inheritance}";
            return classHeader;
        }

        private static string ToTabs(int tabCount) => new string(' ', tabCount * 4);

    }
}
