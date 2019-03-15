using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WithoutHaste.DataFiles.DotNet;

namespace DataFilesTest
{
	[TestClass]
	public class DotNetDocumentationFileTests
	{
		private const string ASSEMBLY_NAME = "Test";

		protected class InheritanceClassA
		{
			public int FieldA;
			public string PropertyA { get; set; }
			public event EventHandler EventA;
			public virtual void MethodA() { }
		}

		protected interface InheritanceInterfaceC
		{
			double PropertyC { get; set; }
			void MethodC();
		}

		protected class InheritanceClassB : InheritanceClassA, InheritanceInterfaceC
		{
			public new int FieldA;
			public new string PropertyA { get; set; }
			public new event EventHandler EventA;
			public override void MethodA() { }

			public double PropertyC { get; set; }
			public void MethodC() { }
		}

		protected class InheritanceClassD : InheritanceClassB
		{
		}

		protected class InheritanceClassE : InheritanceClassD
		{
			public new int FieldA;
			public new string PropertyA { get; set; }
			public new event EventHandler EventA;
			public override void MethodA() { }

			public new double PropertyC { get; set; }
			public new void MethodC() { }
		}

		//----------------------------------

		protected interface InheritanceInterfaceF
		{
			int PropertyA { get; set; }
		}

		protected interface InheritanceInterfaceG
		{
			int PropertyA { get; set; }
		}

		protected class InheritanceClassH : InheritanceInterfaceG, InheritanceInterfaceF
		{
			public int PropertyA { get; set; }
		}

		protected class InheritanceClassH2 : InheritanceInterfaceG, InheritanceInterfaceF
		{
			int InheritanceInterfaceG.PropertyA { get; set; }
			int InheritanceInterfaceF.PropertyA { get; set; }
		}

		//-----------------------------------------

		protected class InheritanceClassI
		{
			public virtual void MethodA<T>(T t) { }
		}

		protected class InheritanceClassJ : InheritanceClassI
		{
			public override void MethodA<U>(U u) { }
		}

		//---------------------------------------------

		[TestInitialize]
		public void Initialize()
		{
#if DATAFILES_TARGET_20 || DATAFILES_TARGET_30
			DotNetSettings.UseDefaultQualifiedNameConverter(false);
#else
			DotNetSettings.QualifiedNameConverter = null;
#endif
		}

		[TestCleanup]
		public void Cleanup()
		{
#if DATAFILES_TARGET_20 || DATAFILES_TARGET_30
			DotNetSettings.UseDefaultQualifiedNameConverter(true);
#else
			DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
#endif
		}

		[TestMethod]
		public void DotNetDocumentationFile_LoadFromFile()
		{
			//arrange
			string filename = "data/DotNetDocumentationFile_Assembly.xml";
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			//assert
			Assert.AreEqual(ASSEMBLY_NAME, file.AssemblyName);
		}

		[TestMethod]
		public void DotNetDocumentationFile_LoadFromXDocument()
		{
			//arrange
			XDocument document = XDocument.Load("data/DotNetDocumentationFile_Assembly.xml", LoadOptions.PreserveWhitespace);
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(document);
			//assert
			Assert.AreEqual(ASSEMBLY_NAME, file.AssemblyName);
		}

		[TestMethod]
		public void DotNetDocumentationFile_CommentFromXml_Full()
		{
			//arrange
			XDocument document = XDocument.Load("data/DotNetDocumentationFile_Full.xml", LoadOptions.PreserveWhitespace);
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(document);
			//assert
			Assert.AreEqual(5, file.TypeCount);

			DotNetType typeA = file.Types[0];
			Assert.AreEqual(2, typeA.NestedTypeCount);
			Assert.AreEqual("Test.TypeA", typeA.Name.FullName);
			Assert.AreEqual(true, typeA.HasComments);
			Assert.AreEqual(1, typeA.Methods.Count);
			Assert.AreEqual(2, typeA.Fields.Count);
			Assert.AreEqual(3, typeA.Properties.Count);
			Assert.AreEqual(1, typeA.Events.Count);

			DotNetType typeB = typeA.NestedTypes[0];
			Assert.AreEqual(1, typeB.NestedTypeCount);
			Assert.AreEqual("Test.TypeA.NestedTypeB", typeB.Name.FullName);
			Assert.AreEqual(true, typeB.HasComments);
			Assert.AreEqual(1, typeB.Methods.Count);

			DotNetType typeC = typeB.NestedTypes[0];
			Assert.AreEqual(0, typeC.NestedTypeCount);
			Assert.AreEqual("Test.TypeA.NestedTypeB.SubNestedTypeC", typeC.Name.FullName);
			Assert.AreEqual(true, typeC.HasComments);
			Assert.AreEqual(2, typeC.Methods.Count);

			DotNetType typeD = file.Types[1];
			Assert.AreEqual("Test.SingleGenericTypeD<T>", typeD.Name.FullName);
			Assert.AreEqual(false, typeD.HasComments);
			Assert.AreEqual(1, typeD.Methods.Count);

			DotNetType typeE = file.Types[2];
			Assert.AreEqual("Test.DoubleGenericTypeE<T,U>", typeE.Name.FullName);
			Assert.AreEqual(false, typeE.HasComments);
			Assert.AreEqual(3, typeE.Methods.Count);

			DotNetMethod methodAA = typeA.Methods[0];
			Assert.AreEqual("Test.TypeA.MethodAA", methodAA.Name.FullName);
			Assert.AreEqual(true, methodAA.HasComments);
			Assert.AreEqual(false, methodAA is DotNetMethodConstructor);
			Assert.AreEqual(false, methodAA is DotNetMethodOperator);
			Assert.AreEqual(0, methodAA.MethodName.Parameters.Count);

			DotNetMethod methodBA = typeB.Methods[0];
			Assert.AreEqual("Test.TypeA.NestedTypeB.MethodBA", methodBA.Name.FullName);
			Assert.AreEqual(false, methodBA.HasComments);
			Assert.AreEqual(false, methodBA is DotNetMethodConstructor);
			Assert.AreEqual(false, methodBA is DotNetMethodOperator);
			Assert.AreEqual(2, methodBA.MethodName.Parameters.Count);

			DotNetMethod methodCA = typeC.Methods[0];
			Assert.AreEqual("Test.TypeA.NestedTypeB.SubNestedTypeC.MethodCA", methodCA.Name.FullName);
			Assert.AreEqual(false, methodCA.HasComments);
			Assert.AreEqual(false, methodCA is DotNetMethodConstructor);
			Assert.AreEqual(false, methodCA is DotNetMethodOperator);
			Assert.AreEqual(1, methodCA.MethodName.Parameters.Count);

			DotNetMethod methodCConstructor = typeC.Methods[1];
			Assert.AreEqual("Test.TypeA.NestedTypeB.SubNestedTypeC.#ctor", methodCConstructor.Name.FullName);
			Assert.AreEqual(false, methodCConstructor.HasComments);
			Assert.AreEqual(true, methodCConstructor is DotNetMethodConstructor);
			Assert.AreEqual(false, methodCConstructor is DotNetMethodOperator);
			Assert.AreEqual(0, methodCConstructor.MethodName.Parameters.Count);

			DotNetMethod methodDAddition = typeD.Methods[0];
			Assert.AreEqual("Test.SingleGenericTypeD<T>.op_Addition", methodDAddition.Name.FullName);
			Assert.AreEqual(false, methodDAddition.HasComments);
			Assert.AreEqual(false, methodDAddition is DotNetMethodConstructor);
			Assert.AreEqual(true, methodDAddition is DotNetMethodOperator);
			Assert.AreEqual(2, methodDAddition.MethodName.Parameters.Count);
			Assert.AreEqual("T", methodDAddition.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("Test.SingleGenericTypeD<T>", methodDAddition.MethodName.Parameters[1].FullTypeName);

			DotNetMethod methodEConstructor = typeE.Methods[0];
			Assert.AreEqual("Test.DoubleGenericTypeE<T,U>.#ctor", methodEConstructor.Name.FullName);
			Assert.AreEqual(false, methodEConstructor.HasComments);
			Assert.AreEqual(true, methodEConstructor is DotNetMethodConstructor);
			Assert.AreEqual(false, methodEConstructor is DotNetMethodOperator);
			Assert.AreEqual(2, methodEConstructor.MethodName.Parameters.Count);
			Assert.AreEqual("U", methodEConstructor.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("T", methodEConstructor.MethodName.Parameters[1].FullTypeName);

			DotNetMethod methodEA = typeE.Methods[1];
			Assert.AreEqual("Test.DoubleGenericTypeE<T,U>.MethodEA<A>", methodEA.Name.FullName);
			Assert.AreEqual(false, methodEA.HasComments);
			Assert.AreEqual(false, methodEA is DotNetMethodConstructor);
			Assert.AreEqual(false, methodEA is DotNetMethodOperator);
			Assert.AreEqual(3, methodEA.MethodName.Parameters.Count);
			Assert.AreEqual("System.String", methodEA.MethodName.Parameters[0].FullTypeName);
			Assert.AreEqual("A", methodEA.MethodName.Parameters[1].FullTypeName);
			Assert.AreEqual("U", methodEA.MethodName.Parameters[2].FullTypeName);

			DotNetMethod methodEB = typeE.Methods[2];
			Assert.AreEqual("Test.DoubleGenericTypeE<T,U>.MethodEB<A>", methodEB.Name.FullName);
			Assert.AreEqual(false, methodEB.HasComments);
			Assert.AreEqual(false, methodEB is DotNetMethodConstructor);
			Assert.AreEqual(false, methodEB is DotNetMethodOperator);
			Assert.AreEqual(1, methodEB.MethodName.Parameters.Count);
			Assert.AreEqual("System.Collections.Generic.List<Test.SingleGenericTypeD<T>>", methodEB.MethodName.Parameters[0].FullTypeName);
		}

		[TestMethod]
		public void DotNetDocumentationFile_SmokeTest_EarlyDocsTest()
		{
			//arrange
			string xmlDocumentationFilename = "../../../../EarlyDocs/Test/bin/Debug/Test.XML";
			string dllFilename = "../../../../EarlyDocs/Test/bin/Debug/Test.dll";
			//act
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			xmlDocumentation.AddAssemblyInfo(dllFilename);
			//assert
			Assert.IsTrue(xmlDocumentation.Types.Count(t => t.Category != TypeCategory.Unknown) > 0);
		}

		[TestMethod]
		public void DotNetDocumentationFile_SmokeTest_DataFiles()
		{
			//arrange
			string xmlDocumentationFilename = "../../../DataFiles/bin/Debug/WithoutHaste.DataFiles.XML";
			string dllFilename = "../../../DataFiles/bin/Debug/WithoutHaste.DataFiles.dll";
			//act
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			//xmlDocumentation.AddAssemblyForReference("System.Xml");
			xmlDocumentation.AddAssemblyInfo(dllFilename);
			//assert
			Assert.IsTrue(xmlDocumentation.Types.Count(t => t.Category != TypeCategory.Unknown) > 0);
		}

		[TestMethod]
		public void DotNetDocumentationFile_SmokeTest_Drawing_Colors()
		{
			//arrange
			string xmlDocumentationFilename = "../../../../WithoutHaste.Drawing.Colors/Colors/bin/Debug/WithoutHaste.Drawing.Colors.XML";
			string dllFilename = "../../../../WithoutHaste.Drawing.Colors/Colors/bin/Debug/WithoutHaste.Drawing.Colors.dll";
			//act
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			xmlDocumentation.AddAssemblyInfo(dllFilename);
			//assert
			Assert.IsTrue(xmlDocumentation.Types.Count(t => t.Category != TypeCategory.Unknown) > 0);
		}

		[TestMethod]
		public void DotNetDocumentationFile_SmokeTest_Drawing_Shapes()
		{
			//arrange
			string xmlDocumentationFilename = "../../../../WithoutHaste.Drawing.Shapes/Shapes/bin/Debug/WithoutHaste.Drawing.Shapes.XML";
			string dllFilename = "../../../../WithoutHaste.Drawing.Shapes/Shapes/bin/Debug/WithoutHaste.Drawing.Shapes.dll";
			//act
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			xmlDocumentation.AddAssemblyInfo(dllFilename);
			//assert
			Assert.IsTrue(xmlDocumentation.Types.Count(t => t.Category != TypeCategory.Unknown) > 0);
		}

		[TestMethod]
		public void DotNetDocumentationFile_SmokeTest_Windows_GUI()
		{
			//arrange
			string xmlDocumentationFilename = "../../../../WithoutHaste.Windows.GUI/GUI/bin/Debug/WithoutHaste.Windows.GUI.XML";
			string dllFilename = "../../../../WithoutHaste.Windows.GUI/GUI/bin/Debug/WithoutHaste.Windows.GUI.dll";
			//act
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			xmlDocumentation.AddAssemblyInfo(dllFilename);
			//assert
			Assert.IsTrue(xmlDocumentation.Types.Count(t => t.Category != TypeCategory.Unknown) > 0);
		}

		[TestMethod]
		public void DotNetDocumentationFile_OrphanedMember_Ignore()
		{
			//arrange
			string filename = "data/DotNetDocumentationFile_OrphanedMember.xml";
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			//assert
			Assert.AreEqual(0, file.Types.Count);
		}

		[TestMethod]
		public void DotNetDocumentationFile_DuplicateComments_OneLevelDeep()
		{
			//arrange
			string filename = "data/DotNetDocumentationFile_DuplicateComments_OneLevelDeep.xml";
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);

			List<DotNetMember> members = new List<DotNetMember>();
			foreach(DotNetType type in file.Types)
			{
				members.Add(type);
				members.AddRange(type.Methods);
				members.AddRange(type.Fields);
				members.AddRange(type.Properties);
				members.AddRange(type.Events);
			}
			DotNetMember typeA = members.OfType<DotNetType>().FirstOrDefault(m => m.Name.LocalName == "TypeA");
			DotNetMember methodA = members.OfType<DotNetMethod>().FirstOrDefault(m => m.Name.LocalName == "MethodA");
			DotNetMember fieldA = members.OfType<DotNetField>().FirstOrDefault(m => m.Name.LocalName == "FieldA");
			DotNetMember propertyA = members.OfType<DotNetProperty>().FirstOrDefault(m => m.Name.LocalName == "PropertyA");
			DotNetMember eventA = members.OfType<DotNetEvent>().FirstOrDefault(m => m.Name.LocalName == "EventA");

			//assert
			int count = 0;
			foreach(DotNetMember member in members)
			{
				if(member.Name.LocalName.EndsWith("FromTypeA"))
					Assert.AreEqual(typeA.SummaryComments[0], member.SummaryComments[0]);
				else if(member.Name.LocalName.EndsWith("FromMethodA"))
					Assert.AreEqual(methodA.SummaryComments[0], member.SummaryComments[0]);
				else if(member.Name.LocalName.EndsWith("FromFieldA"))
					Assert.AreEqual(fieldA.SummaryComments[0], member.SummaryComments[0]);
				else if(member.Name.LocalName.EndsWith("FromPropertyA"))
					Assert.AreEqual(propertyA.SummaryComments[0], member.SummaryComments[0]);
				else if(member.Name.LocalName.EndsWith("FromEventA"))
					Assert.AreEqual(eventA.SummaryComments[0], member.SummaryComments[0]);
				else
					continue;
				count++;
			}
			Assert.AreEqual(25, count);
		}

		[TestMethod]
		public void DotNetDocumentationFile_DuplicateComments_MultipleLevelsDeep()
		{
			//arrange
			string filename = "data/DotNetDocumentationFile_DuplicateComments_MultipleLevelsDeep.xml";
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			DotNetType typeA = file.Types.FirstOrDefault(m => m.Name.LocalName == "TypeA");
			//assert
			int count = 0;
			foreach(DotNetType type in file.Types)
			{
				if(type == typeA)
					continue;
				Assert.AreEqual(typeA.SummaryComments[0], type.SummaryComments[0]);
				count++;
			}
			Assert.AreEqual(5, count);
		}

		[TestMethod]
		public void DotNetDocumentationFile_DuplicateComments_Loop()
		{
			//arrange
			string filename = "data/DotNetDocumentationFile_DuplicateComments_Loop.xml";
			//act
			DotNetDocumentationFile file = new DotNetDocumentationFile(filename);
			DotNetType typeA = file.Types.FirstOrDefault(m => m.Name.LocalName == "TypeA");
			DotNetType typeB = file.Types.FirstOrDefault(m => m.Name.LocalName == "TypeB");
			DotNetType typeC = file.Types.FirstOrDefault(m => m.Name.LocalName == "TypeC");
			//assert
			Assert.AreNotEqual(typeA.FloatingComments[0], typeB.FloatingComments[0]);
			Assert.AreNotEqual(typeB.FloatingComments[0], typeC.FloatingComments[0]);
			Assert.AreNotEqual(typeC.FloatingComments[0], typeA.FloatingComments[0]);
		}

		[TestMethod]
		public void DotNetDocumentationFile_InheritComments_OneLevelDeep()
		{
			//arrange
			string xmlDocumentationFilename = "data/DotNetDocumentationFile_InheritComments_OneLevelDeep.xml";
			//act
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			DotNetType inheritanceTypeA = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceClassA");
			inheritanceTypeA.AddAssemblyInfo(typeof(InheritanceClassA), inheritanceTypeA.Name);
			DotNetType inheritanceTypeB = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceClassB");
			inheritanceTypeB.AddAssemblyInfo(typeof(InheritanceClassB), inheritanceTypeB.Name);
			DotNetType inheritanceTypeC = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceInterfaceC");
			inheritanceTypeC.AddAssemblyInfo(typeof(InheritanceInterfaceC), inheritanceTypeC.Name);
			xmlDocumentation.ResolveInheritedComments();
			//assert
			Assert.AreEqual(inheritanceTypeA.SummaryComments[0], inheritanceTypeB.SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeA.Fields[0].SummaryComments[0], inheritanceTypeB.Fields[0].SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeA.Properties[0].SummaryComments[0], inheritanceTypeB.Properties.First(p => p.Name.LocalName == "PropertyA").SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeA.Events[0].SummaryComments[0], inheritanceTypeB.Events[0].SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeA.Methods[0].SummaryComments[0], inheritanceTypeB.Methods.First(p => p.Name.LocalName == "MethodA").SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeC.Properties[0].SummaryComments[0], inheritanceTypeB.Properties.First(p => p.Name.LocalName == "PropertyC").SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeC.Methods[0].SummaryComments[0], inheritanceTypeB.Methods.First(p => p.Name.LocalName == "MethodC").SummaryComments[0]);
		}

		[TestMethod]
		public void DotNetDocumentationFile_InheritComments_MultipleLevelsDeep()
		{
			//arrange
			string xmlDocumentationFilename = "data/DotNetDocumentationFile_InheritComments_MultipleLevelsDeep.xml";
			//act
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			DotNetType inheritanceTypeA = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceClassA");
			inheritanceTypeA.AddAssemblyInfo(typeof(InheritanceClassA), inheritanceTypeA.Name);
			DotNetType inheritanceTypeB = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceClassB");
			inheritanceTypeB.AddAssemblyInfo(typeof(InheritanceClassB), inheritanceTypeB.Name);
			DotNetType inheritanceTypeC = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceInterfaceC");
			inheritanceTypeC.AddAssemblyInfo(typeof(InheritanceInterfaceC), inheritanceTypeC.Name);
			DotNetType inheritanceTypeD = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceClassD");
			inheritanceTypeD.AddAssemblyInfo(typeof(InheritanceClassD), inheritanceTypeD.Name);
			DotNetType inheritanceTypeE = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceClassE");
			inheritanceTypeE.AddAssemblyInfo(typeof(InheritanceClassE), inheritanceTypeE.Name);
			xmlDocumentation.ResolveInheritedComments();
			//assert
			Assert.AreEqual(inheritanceTypeA.SummaryComments[0], inheritanceTypeB.SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeA.Fields[0].SummaryComments[0], inheritanceTypeB.Fields[0].SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeA.Properties[0].SummaryComments[0], inheritanceTypeB.Properties.First(p => p.Name.LocalName == "PropertyA").SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeA.Events[0].SummaryComments[0], inheritanceTypeB.Events[0].SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeA.Methods[0].SummaryComments[0], inheritanceTypeB.Methods.First(p => p.Name.LocalName == "MethodA").SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeC.Properties[0].SummaryComments[0], inheritanceTypeB.Properties.First(p => p.Name.LocalName == "PropertyC").SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeC.Methods[0].SummaryComments[0], inheritanceTypeB.Methods.First(p => p.Name.LocalName == "MethodC").SummaryComments[0]);

			Assert.AreEqual(inheritanceTypeA.SummaryComments[0], inheritanceTypeD.SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeA.SummaryComments[0], inheritanceTypeE.SummaryComments[0]);
			Assert.AreEqual(1, inheritanceTypeE.Fields[0].SummaryComments.Count);
			Assert.AreEqual(inheritanceTypeA.Fields[0].SummaryComments[0], inheritanceTypeE.Fields[0].SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeA.Properties[0].SummaryComments[0], inheritanceTypeE.Properties.First(p => p.Name.LocalName == "PropertyA").SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeA.Events[0].SummaryComments[0], inheritanceTypeE.Events[0].SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeA.Methods[0].SummaryComments[0], inheritanceTypeE.Methods.First(p => p.Name.LocalName == "MethodA").SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeC.Properties[0].SummaryComments[0], inheritanceTypeE.Properties.First(p => p.Name.LocalName == "PropertyC").SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeC.Methods[0].SummaryComments[0], inheritanceTypeE.Methods.First(p => p.Name.LocalName == "MethodC").SummaryComments[0]);
		}

		[TestMethod]
		public void DotNetDocumentationFile_InheritComments_OrderOfInterfaces()
		{
			//arrange
			string xmlDocumentationFilename = "data/DotNetDocumentationFile_InheritComments_OrderOfInterfaces.xml";
			//act
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			DotNetType inheritanceTypeF = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceInterfaceF");
			inheritanceTypeF.AddAssemblyInfo(typeof(InheritanceInterfaceF), inheritanceTypeF.Name);
			DotNetType inheritanceTypeG = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceInterfaceG");
			inheritanceTypeG.AddAssemblyInfo(typeof(InheritanceInterfaceG), inheritanceTypeG.Name);
			DotNetType inheritanceTypeH = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceClassH");
			inheritanceTypeH.AddAssemblyInfo(typeof(InheritanceClassH), inheritanceTypeH.Name);
			xmlDocumentation.ResolveInheritedComments();
			//assert
			Assert.AreEqual(inheritanceTypeG.Properties[0].SummaryComments[0], inheritanceTypeH.Properties.First(p => p.Name.LocalName == "PropertyA").SummaryComments[0]);
		}

		[TestMethod]
		public void DotNetDocumentationFile_InheritComments_NoBaseType()
		{
			//arrange
			string xmlDocumentationFilename = "data/DotNetDocumentationFile_InheritComments_NoBaseType.xml";
			//act
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			DotNetType inheritanceTypeA = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceClassA");
			inheritanceTypeA.AddAssemblyInfo(typeof(InheritanceClassA), inheritanceTypeA.Name);
			xmlDocumentation.ResolveInheritedComments();
			//assert
			Assert.AreEqual(CommentTag.InheritDoc, inheritanceTypeA.FloatingComments[0].Tag);
		}

		//can't figure out how to test an inheritance loop, since that information comes from the assembly and loops are not allowed

		[TestMethod]
		public void DotNetDocumentationFile_InheritComments_DifferentGenericAliases()
		{
			//arrange
			string xmlDocumentationFilename = "data/DotNetDocumentationFile_InheritComments_DifferentGenericAliases.xml";
			//act
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			DotNetType inheritanceTypeI = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceClassI");
			inheritanceTypeI.AddAssemblyInfo(typeof(InheritanceClassI), inheritanceTypeI.Name);
			DotNetType inheritanceTypeJ = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceClassJ");
			inheritanceTypeJ.AddAssemblyInfo(typeof(InheritanceClassJ), inheritanceTypeJ.Name);
			xmlDocumentation.ResolveInheritedComments();
			//assert
			Assert.AreEqual(inheritanceTypeI.Methods[0].SummaryComments[0], inheritanceTypeJ.Methods[0].SummaryComments[0]);
		}

		[TestMethod]
		public void DotNetDocumentationFile_InheritComments_ExplicitInterfaces()
		{
			//arrange
			string xmlDocumentationFilename = "data/DotNetDocumentationFile_InheritComments_ExplicitInterfaces.xml";
			//act
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			DotNetType inheritanceTypeF = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceInterfaceF");
			inheritanceTypeF.AddAssemblyInfo(typeof(InheritanceInterfaceF), inheritanceTypeF.Name);
			DotNetType inheritanceTypeG = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceInterfaceG");
			inheritanceTypeG.AddAssemblyInfo(typeof(InheritanceInterfaceG), inheritanceTypeG.Name);
			DotNetType inheritanceTypeH2 = xmlDocumentation.Types.First(t => t.Name.LocalName == "InheritanceClassH2");
			inheritanceTypeH2.AddAssemblyInfo(typeof(InheritanceClassH2), inheritanceTypeH2.Name);
			xmlDocumentation.ResolveInheritedComments();
			//assert
			Assert.AreEqual(inheritanceTypeF.Properties[0].SummaryComments[0], inheritanceTypeH2.Properties.First(p => p.Name.ExplicitInterface.LocalName == "InheritanceInterfaceF").SummaryComments[0]);
			Assert.AreEqual(inheritanceTypeG.Properties[0].SummaryComments[0], inheritanceTypeH2.Properties.First(p => p.Name.ExplicitInterface.LocalName == "InheritanceInterfaceG").SummaryComments[0]);
		}
	}
}
