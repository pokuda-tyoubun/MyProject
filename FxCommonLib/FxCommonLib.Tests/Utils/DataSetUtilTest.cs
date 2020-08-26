using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FxCommonLib.Utils;
using System.Data;

namespace FxCommonLib.Tests.Utils {
    [TestClass]
    public class DataSetUtilTest {
        [TestMethod]
        public void TestExtractMachingData() {
            //テストデータ作成
            DataTable srcDt = new DataTable();
            srcDt.Columns.Add("key", typeof(string));
            srcDt.Columns.Add("value", typeof(string));
            DataRow dr = srcDt.NewRow();
            dr["key"] = "key1";
            dr["value"] = "value1";
            srcDt.Rows.Add(dr);
            dr = srcDt.NewRow();
            dr["key"] = "key2";
            dr["value"] = "value2";
            srcDt.Rows.Add(dr);
            dr = srcDt.NewRow();
            dr["key"] = "key3";
            dr["value"] = "value3";
            srcDt.Rows.Add(dr);
            srcDt.AcceptChanges();

            DataTable matchDt = new DataTable();
            matchDt.Columns.Add("code", typeof(string));
            matchDt.Columns.Add("name", typeof(string));
            dr = matchDt.NewRow();
            dr["code"] = "key3";
            dr["name"] = "value3";
            matchDt.Rows.Add(dr);
            dr = matchDt.NewRow();
            dr["code"] = "key2";
            dr["name"] = "value2";
            matchDt.Rows.Add(dr);
            dr = matchDt.NewRow();
            dr["code"] = "key4";
            dr["name"] = "value4";
            matchDt.Rows.Add(dr);
            matchDt.AcceptChanges();

            DataTable result = DataSetUtil.ExtractMachingData(srcDt, matchDt, "key", "code");

            Assert.AreEqual(2, result.Rows.Count);
            Assert.AreEqual("key2", result.Rows[0]["key"]);
            Assert.AreEqual("key3", result.Rows[1]["key"]);
        }
        [TestMethod]
        public void TestGetSumGroupByTable() {
            DataTable srcDt = new DataTable();
            srcDt.Columns.Add("key", typeof(string));
            srcDt.Columns.Add("value1", typeof(int));
            srcDt.Columns.Add("value2", typeof(string));
            DataRow dr = srcDt.NewRow();
            dr["key"] = "key1";
            dr["value1"] = "10";
            dr["value2"] = "aaa";
            srcDt.Rows.Add(dr);
            dr = srcDt.NewRow();
            dr["key"] = "key1";
            dr["value1"] = "20";
            dr["value2"] = "bbb";
            srcDt.Rows.Add(dr);
            dr = srcDt.NewRow();
            dr["key"] = "key2";
            dr["value1"] = "20";
            dr["value2"] = "bbb";
            srcDt.Rows.Add(dr);
            dr = srcDt.NewRow();
            dr["key"] = "key2";
            dr["value1"] = "30";
            dr["value2"] = "ccc";
            srcDt.Rows.Add(dr);
            srcDt.AcceptChanges();

            DataTable retDt = DataSetUtil.GetSumGroupByTable(srcDt, "key", "value1");

            Assert.AreEqual(2, retDt.Rows.Count);
            Assert.AreEqual(retDt.Rows[0]["key"], "key1");
            Assert.AreEqual(retDt.Rows[1]["key"], "key2");
            Assert.AreEqual(retDt.Rows[0]["value1"], 30);
            Assert.AreEqual(retDt.Rows[1]["value1"], 50);
        }
        [TestMethod]
        public void TestGetMinGroupByTable() {
            DataTable srcDt = new DataTable();
            srcDt.Columns.Add("key", typeof(string));
            srcDt.Columns.Add("value1", typeof(int));
            srcDt.Columns.Add("value2", typeof(string));
            DataRow dr = srcDt.NewRow();
            dr["key"] = "key1";
            dr["value1"] = "10";
            dr["value2"] = "aaa";
            srcDt.Rows.Add(dr);
            dr = srcDt.NewRow();
            dr["key"] = "key1";
            dr["value1"] = "20";
            dr["value2"] = "bbb";
            srcDt.Rows.Add(dr);
            dr = srcDt.NewRow();
            dr["key"] = "key2";
            dr["value1"] = "20";
            dr["value2"] = "bbb";
            srcDt.Rows.Add(dr);
            dr = srcDt.NewRow();
            dr["key"] = "key2";
            dr["value1"] = "30";
            dr["value2"] = "ccc";
            srcDt.Rows.Add(dr);
            srcDt.AcceptChanges();

            DataTable retDt = DataSetUtil.GetMinGroupByTable(srcDt, "key", "value2");

            Assert.AreEqual(2, retDt.Rows.Count);
            Assert.AreEqual(retDt.Rows[0]["key"], "key1");
            Assert.AreEqual(retDt.Rows[1]["key"], "key2");
            Assert.AreEqual(retDt.Rows[0]["value2"], "aaa");
            Assert.AreEqual(retDt.Rows[1]["value2"], "bbb");
        }
        [TestMethod]
        public void TestGetMinGroupByTable2() {
            DataTable srcDt = new DataTable();
            srcDt.Columns.Add("key", typeof(string));
            srcDt.Columns.Add("value1", typeof(int));
            srcDt.Columns.Add("value2", typeof(string));
            DataRow dr = srcDt.NewRow();
            dr["key"] = "12345678d";
            dr["value1"] = "10";
            dr["value2"] = "aaa";
            srcDt.Rows.Add(dr);
            dr = srcDt.NewRow();
            dr["key"] = "12345678c";
            dr["value1"] = "20";
            dr["value2"] = "bbb";
            srcDt.Rows.Add(dr);
            dr = srcDt.NewRow();
            dr["key"] = "12345678b";
            dr["value1"] = "20";
            dr["value2"] = "bbb";
            srcDt.Rows.Add(dr);
            dr = srcDt.NewRow();
            dr["key"] = "12345678a";
            dr["value1"] = "30";
            dr["value2"] = "ccc";
            srcDt.Rows.Add(dr);
            srcDt.AcceptChanges();

            DataTable retDt = DataSetUtil.GetMinGroupByTable2(srcDt, "key", "value2");

            Assert.AreEqual(1, retDt.Rows.Count);
            Assert.AreEqual(retDt.Rows[0]["key"], "12345");
            Assert.AreEqual(retDt.Rows[0]["value2"], "aaa");
        }
        [TestMethod]
        public void TestGetColumnNameList() {
            var dsu = new DataSetUtil();

            DataTable t1 = new DataTable();
            t1.Columns.Add("key");
            t1.Columns.Add("value");
            var ret = dsu.GetColumnNameList(t1);

            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(ret[0], "key");
            Assert.AreEqual(ret[1], "value");
        }
        [TestMethod]
        public void TestDistinctRow() {
            var dsu = new DataSetUtil();

            DataTable t1 = new DataTable();
            t1.Columns.Add("key");
            t1.Columns.Add("value");
            DataTable t2 = t1.Clone();

            DataRow dr = t1.NewRow();
            dr["key"] = "a";
            dr["value"] = "1";
            t1.Rows.Add(dr);
            dr = t1.NewRow();
            dr["key"] = "b";
            dr["value"] = "1";
            t1.Rows.Add(dr);
            t1.AcceptChanges();

            dr = t2.NewRow();
            dr["key"] = "b";
            dr["value"] = "1";
            t2.Rows.Add(dr);
            dr = t2.NewRow();
            dr["key"] = "c";
            dr["value"] = "1";
            t2.Rows.Add(dr);
            t2.AcceptChanges();

            //重複ありのマージ
            t1.Merge(t2);
            t1.AcceptChanges();

            var ret = dsu.DistinctRow(t1);

            Assert.AreEqual(3, ret.Rows.Count);
        }

        private enum SapOrderColIndex : int {
            [EnumLabel("列1")]
            列1 = 0,
            [EnumLabel("列2")]
            列2,
            [EnumLabel("列3")]
            列3
        }
        [TestMethod]
        public void TestCreateBlankTbl() {
            var dsu = new DataSetUtil();
            DataTable retTbl = dsu.CreateBlankTbl(typeof(SapOrderColIndex));

            Assert.AreEqual(3, retTbl.Columns.Count);
            Assert.AreEqual("列2", retTbl.Columns[1].ColumnName);
        }
    }
}
