using Net.FreeLibrary.Core;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Net.FreeLibrary.Control
{
    public static class ControlExtension
    {

        #region [ PrepareGrid method ]

        public static void PrepareGrid(this DataGridView grid, string columnListSource, char delimiter, string invisibleText)
        {
            try
            {
                columnListSource = string.Format("{0}", columnListSource);
                string[] cols = columnListSource.Split(new char[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

                if (cols == null)
                    cols = new string[] { };

                if (cols.Length == grid.Columns.Count)
                {
                    if (string.IsNullOrWhiteSpace(invisibleText))
                    {
                        for (int counter = 0; counter < cols.Length; counter++)
                        {
                            grid.Columns[counter].HeaderText = cols[counter];
                        }
                    }
                    else
                    {
                        for (int counter = 0; counter < cols.Length; counter++)
                        {
                            grid.Columns[counter].HeaderText = cols[counter];
                            if (grid.Columns[counter].HeaderText.Contains(invisibleText))//("[i]"))
                            {
                                grid.Columns[counter].Visible = false;
                            }
                        }
                    }
                }
                else
                    throw new Exception("Columns Count must be equal to Resource Column count.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region [ LoadString2ComboBox method ]

        public static void LoadString2ComboBox(this ComboBox cmbx, string resource, char keyLimiter, char valueLimiter)
        {
            try
            {
                if (cmbx == null)
                    throw new Exception("Combobox can not be null.");

                if (resource == null)
                {
                    cmbx.DataSource = null;
                    cmbx.Refresh();
                    return;
                }

                List<KeyValue> keys = new List<KeyValue>();
                string[] keysArr = resource.Split(new char[] { keyLimiter }, StringSplitOptions.RemoveEmptyEntries);
                string[] valsArr = null;
                KeyValue keyVal;

                foreach (var item in keysArr)
                {
                    valsArr = null;
                    valsArr = item.Split(new char[] { valueLimiter }, StringSplitOptions.RemoveEmptyEntries);
                    if (valsArr != null)
                    {
                        if (valsArr.Length == 2)
                        {
                            keyVal = new KeyValue() { Key = valsArr[0], Value = valsArr[1] };
                            keys.Add(keyVal);
                        }
                    }
                }

                cmbx.DataSource = keys;
                cmbx.DisplayMember = "Key";
                cmbx.ValueMember = "Value";
                cmbx.SelectedIndex = -1;
                cmbx.Refresh();
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region [ GetSelectedText method ]

        public static string GetSelectedText(this ComboBox cmbx)
        {
            try
            {
                if (cmbx == null)
                    throw new Exception("Combobox can not be null.");

                if (cmbx.SelectedItem == null)
                    return null;

                string str = cmbx.GetItemText(cmbx.SelectedItem);

                return str;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
