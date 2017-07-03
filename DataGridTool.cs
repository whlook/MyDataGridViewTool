using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Tools
{
    class DataGridTool
    {

        private static ComboBox cmb = null;
        private static DataGridView dgv = null;

##region 事件响应
        /// <summary>
        /// 下拉控件触发,当鼠标点击指定单元格时显示下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void dataGridView_CurrentCellChanged( object sender,EventArgs e)
        {
            cmb.Visible = false;
            cmb.Width = 0;
            if (dgv.CurrentCell.ColumnIndex == 1) // 下拉框出现的单元格（缺少行限制）
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
        /// 下拉框事件，当前选择项变化时更新当前单元格的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void combobox_SelectedIndexChanged(object sender,EventArgs e)
        {
            dgv.CurrentCell.Value = cmb.SelectedItem.ToString();
        }
        /// <summary>
        /// 下拉框事件，当编辑选择项的内容时更新到当前单元格的值（当前单元格最好可以在该处指定）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void combobox1_TextChanged(object sender,EventArgs e)
        {
            dgv.CurrentCell.Value = cmb.Text.ToString();
        }
#endregion
        
        /// <summary>
        ///dataGridView的一些初始设置
        /// </summary>
        /// <param name="dataGridView"></param>
        public static void SetGridView(ref DataGridView dataGridView)
        {

            dgv = dataGridView; // 绑定DataGridView控件

            cmb.Visible = false;
            cmb.Width = 0;
            dataGridView.Controls.Add(cmb); // 在dataGridView中注册该下拉框

            dataGridView.CurrentCellChanged += new System.EventHandler(dataGridView_CurrentCellChanged);  // 增加触发事件
  
            // 增加四列
            dataGridView.Columns.Add(" ", " ");
            dataGridView.Columns.Add(" ", " ");
            dataGridView.Columns.Add(" ", " ");
            dataGridView.Columns.Add(" ", " ");
 
            // 增加两行
            dataGridView.Rows.Add();
            dataGridView.Rows.Add();

            /* 表格相关属性 */
            dataGridView.ColumnHeadersVisible = false; // 隐藏列头

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

#region dataGridView操作
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
        /// <summary>
        ///对所有的单元格进行解析，判断是否符合数值要求（不太安全的实现）
        /// </summary>
        /// <param name="dataGridView"></param>
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
                       
                   } // end foreach
               } // end for
               
           } // end for
           return true;
        } // end ParseCellValue(...)
#endregion
    } // end DataGridTool
}
