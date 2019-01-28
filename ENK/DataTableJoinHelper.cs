using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ENK
{
    public static class DataTableJoinHelper
    {
        public enum JoinType
        {
            Inner = 0,
            Left = 1
        }
       
        public static DataTable JoinTwoDataTablesOnOneColumn(DataTable dtblLeft, DataTable dtblRight, string colToJoinOn, JoinType joinType)
        {
            //Change column name to a temp name so the LINQ for getting row data will work properly.
            string strTempColName = colToJoinOn + "_2";
            if (dtblRight.Columns.Contains(colToJoinOn))
                dtblRight.Columns[colToJoinOn].ColumnName = strTempColName;

            //Get columns from dtblLeft
            DataTable dtblResult = dtblLeft.Clone();

            //Get columns from dtblRight
            var dt2Columns = dtblRight.Columns.OfType<DataColumn>().Select(dc => new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));

            //Get columns from dtblRight that are not in dtblLeft
            var dt2FinalColumns = from dc in dt2Columns.AsEnumerable()
                                  where !dtblResult.Columns.Contains(dc.ColumnName)
                                  select dc;

            //Add the rest of the columns to dtblResult
            dtblResult.Columns.AddRange(dt2FinalColumns.ToArray());

            //No reason to continue if the colToJoinOn does not exist in both DataTables.
            if (!dtblLeft.Columns.Contains(colToJoinOn) || (!dtblRight.Columns.Contains(colToJoinOn) && !dtblRight.Columns.Contains(strTempColName)))
            {
                if (!dtblResult.Columns.Contains(colToJoinOn))
                    dtblResult.Columns.Add(colToJoinOn);
                return dtblResult;
            }

            switch (joinType)
            {

                default:
                case JoinType.Inner:
                    #region Inner
                    //get row data
                    //To use the DataTable.AsEnumerable() extension method you need to add a reference to the System.Data.DataSetExtension assembly in your project. 
                    var rowDataLeftInner = from rowLeft in dtblLeft.AsEnumerable()
                                           join rowRight in dtblRight.AsEnumerable() on rowLeft[colToJoinOn] equals rowRight[strTempColName]
                                           select rowLeft.ItemArray.Concat(rowRight.ItemArray).ToArray();


                    //Add row data to dtblResult
                    foreach (object[] values in rowDataLeftInner)
                        dtblResult.Rows.Add(values);

                    #endregion
                    break;
                case JoinType.Left:
                    #region Left
                    var rowDataLeftOuter = from rowLeft in dtblLeft.AsEnumerable()
                                           join rowRight in dtblRight.AsEnumerable() on rowLeft[colToJoinOn] equals rowRight[strTempColName] into gj
                                           from subRight in gj.DefaultIfEmpty()
                                           select rowLeft.ItemArray.Concat((subRight == null) ? (dtblRight.NewRow().ItemArray) : subRight.ItemArray).ToArray();


                    //Add row data to dtblResult
                    foreach (object[] values in rowDataLeftOuter)
                        dtblResult.Rows.Add(values);

                    #endregion
                    break;
            }

            //Change column name back to original
            dtblRight.Columns[strTempColName].ColumnName = colToJoinOn;

            //Remove extra column from result
            dtblResult.Columns.Remove(strTempColName);

            return dtblResult;
        }
    }
}