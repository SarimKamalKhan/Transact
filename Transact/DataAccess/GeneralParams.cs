using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GeneralParams
    {
        public enum GeneralDBTypes { VarChar, Int, DateTime, Decimal, Cursor, Char, Text, Number }

        public GeneralParams(string Name, int Size, GeneralDBTypes Type, object Value, ParameterDirection Direction)
        {
            try
            {
                ParamName = Name;
                this.Size = Size;
                ParamDBType = Type;
                InputValue = Value;
                ParamDirection = Direction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GeneralParams(string Name, int Size, GeneralDBTypes Type, object Value, ParameterDirection Direction, System.Byte Precision, System.Byte Scale)
        {
            try
            {
                ParamName = Name;
                this.Size = Size;
                ParamDBType = Type;
                InputValue = Value;
                ParamDirection = Direction;
                this.Precision = Precision;
                this.Scale = Scale;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GeneralParams(string Name, GeneralDBTypes Type, object Value)
        {
            try
            {
                ParamName = Name;
                ParamDBType = Type;
                InputValue = Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string sParamName;

        public string ParamName
        {
            get
            {
                return sParamName;
            }
            set
            {
                sParamName = value;
            }
        }

        private int iSize;

        public int Size
        {
            get
            {
                return iSize;
            }
            set
            {
                iSize = value;
            }
        }

        private GeneralDBTypes eParamDBType;

        public GeneralDBTypes ParamDBType
        {
            get
            {
                return eParamDBType;
            }
            set
            {
                eParamDBType = value;
            }
        }

        private object ParamValue;

        public object InputValue
        {
            get
            {
                return ParamValue;
            }
            set
            {
                ParamValue = value;
            }
        }

        private ParameterDirection sParamDirection;

        public ParameterDirection ParamDirection
        {
            get
            {
                return sParamDirection;
            }
            set
            {
                sParamDirection = value;
            }
        }

        private System.Byte bPrecision;

        public System.Byte Precision
        {
            get
            {
                return bPrecision;
            }
            set
            {
                bPrecision = value;
            }
        }

        private System.Byte bScale;

        public System.Byte Scale
        {
            get
            {
                return bScale;
            }
            set
            {
                bScale = value;
            }
        }

        private string sResult;

        public string OutputValue
        {
            get
            {
                return sResult;
            }
            set
            {
                sResult = value;
            }
        }
    }
}
