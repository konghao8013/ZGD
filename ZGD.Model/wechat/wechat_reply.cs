using System;
using System.Collections.Generic;
using System.Text;

namespace ZGD.Model
{
    public class wechat_reply
    {
        private int _id;
        private string _reply_type;
        private string _reply_key;
        private string _reply_fodder_id;
        private DateTime _addtime;


        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string reply_type
        {
            get { return _reply_type; }
            set { _reply_type = value; }
        }
        public string reply_key
        {
            get { return _reply_key; }
            set { _reply_key = value; }
        }
        public string reply_fodder_id
        {
            get { return _reply_fodder_id; }
            set { _reply_fodder_id = value; }
        }
        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
    }
}
