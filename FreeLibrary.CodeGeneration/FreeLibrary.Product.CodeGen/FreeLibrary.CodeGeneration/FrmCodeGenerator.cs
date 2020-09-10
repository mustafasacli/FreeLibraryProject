using FreeLibrary.CodeGeneration.Source.BO;
using FreeLibrary.CodeGeneration.Source.Configuration;
using FreeLibrary.CodeGeneration.Source.Generate;
using FreeLibrary.CodeGeneration.Source.Printing;
using FreeLibrary.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FreeLibrary.CodeGeneration
{
    public partial class FrmCodeGenerator : Form
    {
        #region [ Private Fields ]

        private volatile bool _isChkSelectAllBusy = false;
        private volatile bool _isLstCheckTableBusy = false;

        private List<Table> allTables;
        private Printer clsPrint;
        private CodeGenerator tableGenerator;

        private Thread thrdSaveSettings, thrdGetTables, thrdGenerate;

        #endregion [ Private Fields ]

        #region [ FrmCodeGenerator Ctor ]

        public FrmCodeGenerator()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            allTables = new List<Table>();
            tableGenerator = new CodeGenerator();
            clsPrint = new Printer();
        }

        #endregion [ FrmCodeGenerator Ctor ]

        #region [ Form Load - 0 ]

        private void FormLoad(object sender, EventArgs e)
        {
            LoadCombo();
        }

        #endregion [ Form Load - 0 ]

        #region [ LoadCombo method ]

        private void LoadCombo()
        {
            try
            {
                cmbConnectionType.Items.Clear();
                foreach (var item in Enum.GetValues(typeof(ConnectionTypes)))
                {
                    cmbConnectionType.Items.Add(item);
                }
                cmbConnectionType.Refresh();
            }
            catch (Exception exc)
            {
                LogException(exc, 0);
            }
        }

        #endregion [ LoadCombo method ]

        #region [ txtNameSpace Text Changing Event ]

        private void TxtNameSpaceTextChangeEvent(object sender, EventArgs e)
        {
            this.clsPrint.NameSpace = this.txtNameSpace.Text;
        }

        #endregion [ txtNameSpace Text Changing Event ]

        #region [ Method of Selected Index Changed of cmbConnectionTypes - 1 ]

        private void CmbConnectionTypeIndexChange(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbConnectionType.SelectedIndex != -1)
                {
                    tableGenerator.ConnType = (ConnectionTypes)cmbConnectionType.SelectedItem;

                    this.txtConnectionString.Text = AppConfiguration.Settings[
                        this.cmbConnectionType.SelectedItem.ToString()].ToString();
                }
                else
                    this.txtConnectionString.Text = string.Empty;
            }
            catch (Exception exc)
            {
                this.LogException(exc, 1);
            }
            finally
            {
                tableGenerator.ConnectionString = this.txtConnectionString.Text;
                CodeGenerator.ConnStr = this.txtConnectionString.Text;
                CodeGenerator.Index = this.cmbConnectionType.SelectedIndex;
            }
        }

        #endregion [ Method of Selected Index Changed of cmbConnectionTypes - 1 ]

        #region [ btnBrowse Click event - 2 ]

        private void Browse(object sender, EventArgs e)
        {
            try
            {
                browserDestinationDialog.Description = "Kaydedilecek klasör";
                DialogResult dialogRes = browserDestinationDialog.ShowDialog();

                if (dialogRes == DialogResult.OK)
                {
                    clsPrint.SavingPath = browserDestinationDialog.SelectedPath;
                    txtSaveToPath.Text = clsPrint.SavingPath;
                }
            }
            catch (Exception exc)
            {
                tbpgLog.Select();
                LogException(exc, 2);
            }
        }

        #endregion [ btnBrowse Click event - 2 ]

        #region [ btnGenerate Click event - 3 ]

        private void GenerateClick(object sender, EventArgs e)
        {
            try
            {
                if (NameSpaceController.IsNullOrSpace(clsPrint.SavingPath))
                {
                    MessageBox.Show("Saving Path is not empty.");
                    return;
                }

                if (!NameSpaceController.ControlNameSpace(clsPrint.NameSpace))
                {
                    MessageBox.Show("NameSpace includes forbidden characters.");
                    return;
                }

                chklstTables.Enabled = false;
                SetEnableState();
                thrdGenerate = new Thread(new ThreadStart(this.Generate));
                thrdGenerate.Start();

                //Generate();
                //MessageBox.Show("All Tables created.", "Result Info:");
            }
            catch (Exception exc)
            {
                tbpgLog.Select();
                LogException(exc, 3);
            }
            finally
            {
                chklstTables.Enabled = true;
                SetEnableState(true);
            }
        }

        #endregion [ btnGenerate Click event - 3 ]

        #region [ Generate method ]

        private void Generate()
        {
            try
            {
                List<string> tableNames = new List<string>();
                foreach (var item in chklstTables.CheckedItems)
                {
                    tableNames.Add(item.ToString());
                }

                List<Table> tables = new List<Table>();
                Table tbl;

                for (int tableCounter = 0; tableCounter < allTables.Count; tableCounter++)
                {
                    if (tableNames.Contains(allTables[tableCounter].TableName))
                    {
                        tbl = allTables[tableCounter];
                        tbl.NameSpace = clsPrint.NameSpace;
                        tables.Add(tbl);
                    }
                }

                /*
                for (int tableCounter = 0; tableCounter < tables.Count; tableCounter++)
                {
                    tbl.TableColumns = tableGenerator.GetColumnsOfTable(tbl.TableName);
                    tables[tableCounter] = tbl;
                }
                for (int tableCounter = 0; tableCounter < tables.Count; tableCounter++)
                {
                    tbl = tables[tableCounter];
                    tbl.IdColumn = tableGenerator.GetIdColumnOfTable(tables[tableCounter].TableName);
                    tables[tableCounter] = tbl;
                }
                */
                clsPrint.PrintClassTable(tables);
                StringBuilder sBuilder = new StringBuilder();
                foreach (Table cls in tables)
                {
                    sBuilder.AppendFormat("{0},\n", cls.TableName);
                }
                StringBuilder newBuilder = new StringBuilder(
                    sBuilder.ToString().Substring(0, sBuilder.ToString().Length - 1));
                newBuilder.AppendLine(" tables created.");
                txtLog.AppendText(newBuilder.ToString());
                MessageBox.Show("All Tables created.", "Result Info:");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion [ Generate method ]

        #region [ btnGetTables Click event ]

        private void GetTablesClick(object sender, EventArgs e)
        {
            try
            {
                SetEnableState();
                thrdGetTables = new Thread(new ThreadStart(this.GetTables));
                thrdGetTables.Start();
                //GetTables();
            }
            catch (Exception exc)
            {
                this.tbpgLog.Select();
                this.LogException(exc, 5);
            }
        }

        #endregion [ btnGetTables Click event ]

        #region [ GetTables method ]

        private void GetTables()
        {
            allTables = new List<Table>();
            try
            {
                this.clsPrint.SavingPath = string.Empty;
                this.chklstTables.Items.Clear();
                allTables = tableGenerator.GetTablesAndColumns(tableGenerator.ConnType, tableGenerator.ConnectionString);
            }
            catch (Exception exc)
            {
                this.tbpgLog.Select();
                this.LogException(exc, 5);
            }
            finally
            {
                WriteTables();
                SetEnableState(true);
            }
        }

        #endregion [ GetTables method ]

        #region [ WriteTables method ]

        private void WriteTables()
        {
            try
            {
                _isChkSelectAllBusy = true;
                foreach (Table clazztable in allTables)
                {
                    this.chklstTables.Items.Add(clazztable.TableName, CheckState.Checked);
                }
                chkSelectAll.Checked = true;
            }
            finally
            {
                _isChkSelectAllBusy = false;
            }
        }

        #endregion [ WriteTables method ]

        #region [ LogException method ]

        private void LogException(Exception exc, int exceptionType)
        {
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat(
                    "\nException Date: {0}\n", DateTime.Now.ToString("hh:mm:ss dd.MM.yyyy")).AppendFormat(
                    "Exception Type: {0}\n", exceptionType).AppendFormat(
                    "Exception Message: {0}\n", exc.Message).AppendFormat(
                    "Exception Stack Trace: {0}\n", exc.StackTrace);

                txtLog.AppendText(strBuilder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "An Error has occured.");
            }
        }

        #endregion [ LogException method ]

        #region [ Save Settings - 5 ]

        private void SaveSettings(object sender, EventArgs e)
        {
            //SaveSettings();
            try
            {
                SetEnableState();
                thrdSaveSettings = new Thread(new ThreadStart(this.SaveSetings));
                thrdSaveSettings.Start();
            }
            catch (Exception ex)
            {
                tbpgLog.Select();
                LogException(ex, 5);
            }
            finally
            {
                SetEnableState(true);
            }
        }

        #endregion [ Save Settings - 5 ]

        #region [ SaveSettings method ]

        /// <summary>
        /// Saves Settings.
        /// </summary>
        private void SaveSetings()
        {
            try
            {
                int index = cmbConnectionType.SelectedIndex;
                if (index != -1)
                {
                    clsPrint.SavingPath = string.Empty;
                    //settin
                    AppConfiguration.Settings.SaveSetting(
                        cmbConnectionType.SelectedItem.ToString(), txtConnectionString.Text);
                    // tableGenerator.ConnectionString = txtConnectionString.Text;
                    LoadCombo();
                    cmbConnectionType.SelectedIndex = -1;
                    cmbConnectionType.SelectedIndex = index;
                }
            }
            catch (Exception ex)
            {
                tbpgLog.Select();
                LogException(ex, 5);
            }
        }

        #endregion [ SaveSettings method ]

        //void SaveSettings() { }

        #region [ Select - DeSelect All - 6 ]

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLstCheckTableBusy == true)
                    return;

                _isChkSelectAllBusy = true;
                if (chklstTables.Items == null)
                    return;

                if (chklstTables.Items.Count == 0)
                    return;

                for (int itemCounter = 0; itemCounter < chklstTables.Items.Count; itemCounter++)
                {
                    chklstTables.SetItemChecked(itemCounter, chkSelectAll.Checked);
                }
            }
            catch (Exception ex)
            {
                tbpgLog.Select();
                LogException(ex, 6);
            }
            finally
            {
                _isChkSelectAllBusy = false;
            }
        }

        #endregion [ Select - DeSelect All - 6 ]

        #region [ chklstTables_ItemCheck method - 7 ]

        private void chklstTables_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (_isChkSelectAllBusy == true)
                    return;

                _isLstCheckTableBusy = true;

                if (chklstTables.SelectedItems.Count == chklstTables.Items.Count)
                {
                    chkSelectAll.CheckState = CheckState.Checked;
                    return;
                }

                if (chklstTables.SelectedItems.Count == 0)
                {
                    chkSelectAll.CheckState = CheckState.Unchecked;
                    return;
                }

                chkSelectAll.CheckState = CheckState.Indeterminate;
            }
            catch (Exception ex)
            {
                tbpgLog.Select();
                LogException(ex, 7);
            }
            finally
            {
                _isLstCheckTableBusy = false;
            }
        }

        #endregion [ chklstTables_ItemCheck method - 7 ]

        #region [ SetEnableState method ]

        private void SetEnableState(bool enableState = false)
        {
            btnBrowse.Enabled = enableState;
            btnGenerate.Enabled = enableState;
            btnGetTables.Enabled = enableState;
            btnSaveConnStr.Enabled = enableState;
            cmbConnectionType.Enabled = enableState;
        }

        #endregion [ SetEnableState method ]
    }
}