using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using FreeLibrary.Entity.Extensions;
using FreeLibrary.Entity.Validation;

namespace FreeLibrary.Entity
{
    public abstract class BaseBO : IBaseBO
    {

        #region [ Private Fileds ]

        private List<string> _changeList = null;

        #endregion [ Private Fileds ]


        #region [ BaseBO Ctor ]

        /// <summary>
        /// BaseBO Ctor.
        /// </summary>
        protected BaseBO()
        {
            _changeList = new List<string>();
            this.PropertyChanged += BaseBO_PropertyChanged;
            //this.PropertyChanging += BaseBO_PropertyChanging;
        }

        private void BaseBO_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
        }

        #endregion [ BaseBO Ctor ]


        #region [ GetTableName method ]

        /// <summary>
        /// Gets Table Name Of BaseBO object.
        /// </summary>
        /// <returns>Returns Table Name Of BaseBO object.</returns>
        public virtual string GetTableName()
        {
            throw new NotImplementedException("Table Name is not implemented. You must be create GetTableName method.");
        }

        #endregion [ GetTableName method ]


        #region [ GetIdColumn method ]

        /// <summary>
        ///  Gets Identity Column Name Of BaseBO object.
        /// </summary>
        /// <returns>Returns Identity Column Name Of BaseBO object.</returns>
        public virtual string GetIdColumn()
        {
            throw new NotImplementedException("Id Column is not implemented. You must be create GetIdColumn method.");
        }

        #endregion [ GetIdColumn method ]


        #region [ GetColumnChangeList method ]

        /// <summary>
        /// Gets Column Name list with property changed.
        /// </summary>
        /// <returns>Returns Column Name list with property changed.</returns>
        public List<String> GetColumnChangeList()
        {
            return _changeList;
        }

        #endregion [ GetColumnChangeList method ]


        #region [ Equals method ]

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj">object instance which inherits BaseBO object.</param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            bool result = false;

            if (object.ReferenceEquals(obj.GetType(), this.GetType()))
            {
                BaseBO bo = (BaseBO)obj;
                if (bo.GetTableName().Equals(this.GetTableName()) & bo.GetIdColumn().Equals(this.GetIdColumn()))
                {
                    PropertyInfo propInf = bo.GetType().GetProperty(bo.GetIdColumn());
                    object obj1 = propInf.GetValue(bo);
                    object obj2 = propInf.GetValue(this);
                    result = object.Equals(obj1, obj2);
                }
            }

            return result;
        }

        #endregion [ Equals method ]


        #region [ GetHashCode method ]

        /// <summary>
        /// Gets HashCode of object.
        /// </summary>
        /// <returns>Returns HashCode int.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion [ GetHashCode method ]


        #region [ Serialize method ]

        /// <summary>
        /// Write Object serialization result to a file.
        /// </summary>
        /// <param name="fileName"> File Name for Serialization object writing. </param>
        /// <param name="append">if append is true Serialization continues with append.</param>
        public void Serialize(string fileName, bool append = false)
        {
            try
            {
                //StringBuilder strBuilder = new StringBuilder();
                PropertyInfo[] pInfos = this.GetType().GetProperties();
                string str = string.Empty;
                //strBuilder.AppendLine(String.Format("Name : {0} ", this.GetType().Name));
                str = string.Format("Name : {0} ", this.GetType().Name);
                object objVal;

                foreach (PropertyInfo inf in pInfos)
                {
                    if (inf.Name.Equals(this.GetIdColumn()) == false)
                    {
                        objVal = inf.GetValue(this, null);
                        str = string.Concat(str, " : ", inf.Name, objVal, Environment.NewLine);
                        //strBuilder.AppendLine(String.Format("{0} : {1} ", inf.Name, objVal));
                    }
                }

                FileMode fMode = FileMode.OpenOrCreate;
                if (append)
                {
                    //strBuilder.AppendLine("/* ------------------------------------------------*/");
                    fMode = FileMode.Append;
                    str = string.Concat(str, "/* ------------------------------------------------*/", Environment.NewLine);
                }

                using (StreamWriter outfile = new StreamWriter(new FileStream(fileName, fMode)))
                {
                    //outfile.Write(strBuilder.ToString());
                    outfile.Write(str);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Write Object serialization result to a file.
        /// </summary>
        /// <param name="fileName"> File Name for Serialization object writing. </param>
        public void Serialize(string fileName)
        {
            try
            {
                Serialize(fileName, false);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion [ Serialize method ]


        #region [ BaseBO_PropertyChanged method ]

        private void BaseBO_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            bool check = _changeList.Contains(e.PropertyName);

            if (!check && !string.IsNullOrWhiteSpace(e.PropertyName))
            {
                _changeList.Add(e.PropertyName);
            }
        }

        #endregion [ BaseBO_PropertyChanged method ]


        #region [ PropertyChanged EventHandler ]

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion [ PropertyChanged EventHandler ]


        #region [ OnPropertyChanged method ]

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion [ OnPropertyChanged method ]


        #region [ ChangeSetCount Property ]

        /// <summary>
        /// Gets Count of Changed Property.
        /// </summary>
        public int ChangeSetCount
        {
            get
            {
                int count = _changeList.Count;
                return count;
            }
        }

        #endregion [ ChangeSetCount Property ]


        #region [ IsPropertyChanged method ]

        /// <summary>
        /// Returns true if Value of Property with given name changes, return true; else returns false.
        /// </summary>
        /// <param name="propName">Property Name</param>
        /// <returns>Returns bool object.</returns>
        public bool IsPropertyChanged(string propName)
        {
            bool result = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(propName))
                {
                    result = _changeList.Contains(propName);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return result;
        }

        #endregion [ IsPropertyChanged method ]


        #region [ Clear method ]

        /// <summary>
        /// Clears all change columns list.
        /// </summary>
        public void Clear()
        {
            _changeList.Clear();
            _changeList = new List<string>();
        }

        #endregion [ Clear method ]


        #region [ ValidateObject method ]

        /// <summary>
        /// Make Validation Operation for Insert/Update Object.
        /// </summary>
        /// <param name="isUpdate">Determines Validation is for update operation. 
        /// If true, Validation is for Update operation else Validation Insert Operation.</param>
        /// <returns>Returns IEntityValidationResult object.</returns>
        public IEntityValidationResult ValidateObject(bool isUpdate = false)
        {
            IEntityValidationResult result = this.Validate();

            try
            {
                if (result.FailError == null && result.HasError)
                {
                    if (isUpdate && this.ChangeSetCount > 0)
                    {
                        IList<ValidationResult> resultError = new List<ValidationResult>();
                        List<string> changeSet = this.GetColumnChangeList();

                        IEnumerator<string> members;
                        string errMessage;
                        List<string> lstMembers;

                        foreach (var item in result.Errors)
                        {
                            errMessage = item.ErrorMessage;
                            members = item.MemberNames.GetEnumerator();
                            lstMembers = new List<string>();
                            while (members.MoveNext())
                            {
                                if (changeSet.Contains(members.Current))
                                {
                                    lstMembers.Add(members.Current);
                                }
                            }

                            if (lstMembers.Count > 0)
                            {
                                resultError.Add(new ValidationResult(errMessage, lstMembers.AsEnumerable()));
                            }
                        }

                        result = new EntityValidationResult(resultError);
                        /*
                            string idProp = this.GetIdColumn();
                            if (!string.IsNullOrWhiteSpace(idProp))
                            {
                                PropertyInfo prpId=
                            }
                        */
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return result;
        }

        #endregion


        #region [ ClearChanges method ]

        /// <summary>
        /// if includeIdColumn is true, Clears all change columns list, else Clear changes except id column.
        /// </summary>
        /// <param name="includeIdColumn">id column include determining parameter.</param>
        public void ClearChanges(bool includeIdColumn = false)
        {
            try
            {
                string idColumn = this.GetIdColumn();
                bool containsIdCol = this._changeList.Contains(idColumn);

                this._changeList.Clear();
                this._changeList = new List<string>();

                containsIdCol &= !includeIdColumn;
                containsIdCol &= !string.IsNullOrWhiteSpace(idColumn);

                if (containsIdCol)
                {
                    this._changeList.Add(idColumn);
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion


        #region [ IsNullOrDefault method ]

        public virtual bool IsNullOrDefault()
        {
            return false;
            //Type t = this.GetType();

            //bool result = this.IsNullOrDefault<(t as IBaseBO)>();
            //return result;
        }

        #endregion

    }
}