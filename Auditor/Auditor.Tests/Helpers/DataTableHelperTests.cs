
using NUnit.Framework;
using System;

namespace Auditor.Tests.Helpers
{
    [TestFixture]
    public class DataTableHelperTests
    {
        public class TestClass1 { public string TextField { get; set; } public int NumberField { get; set; } }
        public class TestClass2 { public string TextField2 { get; set; } public int NumberField2 { get; set; } }

        [Test]
        public void ShouldGetDataTableWithTypeName()
        {
            var table = Auditor.Core.Helpers.DataTableHelper.GetDataTable<TestClass1>();

            Assert.AreEqual(typeof(TestClass1).FullName, table.TableName);
        }

        [Test]
        public void NumberOfColumnsShouldMatchNumberOfTypeProperties()
        {
            var table = Auditor.Core.Helpers.DataTableHelper.GetDataTable<TestClass1>();

            Assert.AreEqual(typeof(TestClass1).GetProperties().Length, table.Columns.Count);
        }

        [Test]
        public void FirstColumnShouldMatchFirstTypeProperty()
        {
            var table = Auditor.Core.Helpers.DataTableHelper.GetDataTable<TestClass1>();

            Assert.AreEqual(typeof(TestClass1).GetProperties()[0].Name, table.Columns[0].ColumnName);
        }

        [Test]
        public void ShouldFailForDifferentType()
        {
            var table = Auditor.Core.Helpers.DataTableHelper.GetDataTable<TestClass1>();
            var row = table.NewRow();

            var data2 = new TestClass2 { TextField2 = "tf2", NumberField2 = 1 };
            Assert.That(() => Auditor.Core.Helpers.DataTableHelper.FillRow(row, data2), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void ShouldFillDataRow()
        {
            var table = Auditor.Core.Helpers.DataTableHelper.GetDataTable<TestClass1>();
            var row = table.NewRow();

            var data = new TestClass1 { TextField = "tf1", NumberField = 1 };
            Auditor.Core.Helpers.DataTableHelper.FillRow(row, data);

            Assert.AreEqual(data.TextField, row[0]);
            Assert.AreEqual(data.TextField, row[nameof(data.TextField)]);

            Assert.AreEqual(data.NumberField, row[1]);
            Assert.AreEqual(data.NumberField, row[nameof(data.NumberField)]);
        }
    }
}
