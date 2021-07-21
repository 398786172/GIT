using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OCV
{
    [Serializable]
    public class CustomException : ApplicationException
    {
        int type = 0;
        int value = 0;

        public int Value
        {
            get
            {
                return value;
            }
        }

        public int Type
        {
            get
            {
                return type;
            }
        }
        //构造函数1
        public CustomException()
            : base()
        {
        }
        //构造函数2
        public CustomException(string message)
            : base(message)
        {
        }
        //构造函数3
        public CustomException(string message, Exception inner)
            : base(message, inner)
        {
        }
        //构造函数4,添加自定义数据
        public CustomException(string message, int type, int value)
            : base(message)
        {
            this.type = type;
            this.value = value;
        }
        //序列化
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Type", type);
            info.AddValue("Value", value);
        }

        //反序列化
        public CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            type = info.GetInt32("Type");
            value = info.GetInt32("Value");
        }
        //重写message
        public override string Message
        {
            get
            {
                string s = string.Format("Type:{0},Value:{1}", type, value);
                return base.Message + Environment.NewLine + s;
            }
        }


    }
}
