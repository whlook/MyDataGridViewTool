﻿using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WindowsFormsApplication1
{
    class DataGridTool
    {

        private static ComboBox cmb = new ComboBox();
        private static DataGridView dgv = null;


        /// <summary>
        /// 下拉控件触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void dataGridView_CurrentCellChanged( object sender,EventArgs e)
        {
            cmb.Visible = false;
            cmb.Width = 0;
            if (dgv.CurrentCell.ColumnIndex == 1) // 选定出现的列
            {
                cmb.Left = dgv.GetCellDisplayRectangle(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex, true).Left;
                cmb.Top = dgv.GetCellDisplayRectangle(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex, true).Top;
                cmb.Width = dgv.GetCellDisplayRectangle(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex, true).Width;
                string content ="";
                if(dgv.CurrentCell.Value!=null)
                     content = dgv.CurrentCell.Value.ToString();
                cmb.Text = content;
                cmb.Visible = true;
            }
            else
            {
                cmb.Visible = false;
                cmb.Width = 0;
            }
        }
        /// <summary>
        /// 初始设置gridview特性
        /// </summary>
        /// <param name="dataGridView"></param>
        public static void SetGridView(ref DataGridView dataGridView)
        {

            dgv = dataGridView; // 绑定DataGridView控件

            cmb.Visible = false;
            cmb.Width = 0;
            dataGridView.Controls.Add(cmb);

            dataGridView.CurrentCellChanged += new System.EventHandler(dataGridView_CurrentCellChanged);  // 增加触发事件
  
            // 增加四列
            dataGridView.Columns.Add(" ", " ");
            dataGridView.Columns.Add(" ", " ");
            dataGridView.Columns.Add(" ", " ");
            dataGridView.Columns.Add(" ", " ");
 
            // 增加两行
            dataGridView.Rows.Add();
            dataGridView.Rows.Add();

            dataGridView.ColumnHeadersVisible = false;

            dataGridView.Rows[0].HeaderCell.Value = "T/℃";

            dataGridView.Rows[1].HeaderCell.Value = "系数";

            dataGridView.RowHeadersWidth = 100;

            dataGridView.Height = 68;

            dataGridView.Width = 500;

            dataGridView.AllowUserToResizeColumns = false;

            dataGridView.AllowUserToResizeRows = false;

            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            dataGridView.AllowUserToAddRows = false;
        }
        /// <summary>
        /// 获得指定行列单元的值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public static string GetCellValue(int row,int col,ref DataGridView dataGridView)
        {
            if (row > dataGridView.RowCount-1 || col > dataGridView.ColumnCount-1)
                return "null";
            dataGridView.CurrentCell = dataGridView[col, row];
            return dataGridView.CurrentCell.Value.ToString();
        }
        /// <summary>
        /// 设置指定行列单元的值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="val"></param>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public static bool SetCellValue(int row,int col,string val,ref DataGridView dataGridView)
        {
            if (row > dataGridView.RowCount-1 || col > dataGridView.ColumnCount-1)
                return false;
            dataGridView.CurrentCell = dataGridView[col,row];
            if (dataGridView.CurrentCell.Value != null)
            {
                dataGridView.CurrentCell.Value = val.ToString();
                return true;
            }

            return false;
        }
        /// <summary>
        ///增加一列
        /// </summary>
        /// <param name="dataGridView"></param>
        public static void AddCol(ref DataGridView dataGridView)
        {
            dataGridView.Columns.Add(" ", " ");
        }

        public static bool ParseCellValue(ref DataGridView dataGridView)
        {
           for(int i = 0;i<dataGridView.RowCount;++i)
           {
               for(int j = 0;j<dataGridView.ColumnCount;++j)
               {
                   dataGridView.CurrentCell = dataGridView[j, i];
          
                   string val = "0";

                   if (dataGridView.CurrentCell.Value != null)
                        val = dataGridView.CurrentCell.Value.ToString().Trim();
    
                   if (val.Length == 0)
                       continue;
                   foreach (char v in val)
                   {
                      
                           if(v!='-'&&v!='+'&v!='.')
                           {
                               if (v < '0' || v > '9')
                                   return false;
                           }
                       
                   }
               }
               
           }
           return true;
        }
    }
}
