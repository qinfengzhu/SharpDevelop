﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using ICSharpCode.PythonBinding;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.SharpDevelop.Dom;
using NUnit.Framework;
using PythonBinding.Tests;

namespace PythonBinding.Tests.Parsing
{
	[TestFixture]
	public class ParseClassNestedInsideMethodTestFixture
	{
		ICompilationUnit compilationUnit;
		IClass c;
		
		[SetUp]
		public void SetUpFixture()
		{
			string python =
				"class MyClass:\r\n" +
				"    def firstMethod(self):\r\n" +
				"        class NestedClass:\r\n" +
				"            def firstNestedClassMethod(self):\r\n" +
				"                pass\r\n" +
				"\r\n" +
				"    def secondMethod(self):\r\n" +
				"        pass\r\n" +
				"\r\n";
			
			DefaultProjectContent projectContent = new DefaultProjectContent();
			PythonParser parser = new PythonParser();
			compilationUnit = parser.Parse(projectContent, @"C:\test.py", python);
			if (compilationUnit.Classes.Count > 0) {
				c = compilationUnit.Classes[0];
			}
		}
		
		[Test]
		public void CompilationUnitHasOneClass()
		{
			Assert.AreEqual(1, compilationUnit.Classes.Count);
		}
		
		[Test]
		public void MyClassHasTwoMethods()
		{
			Assert.AreEqual(2, c.Methods.Count);
		}
	}
}
