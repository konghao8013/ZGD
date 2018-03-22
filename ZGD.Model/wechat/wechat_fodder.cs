using System;
using System.Collections.Generic;
using System.Text;

namespace ZGD.Model
{
    public class wechat_fodder
    {
        private int _id;
        private string _fodder_xml;
        private string _fodder_type;
        private DateTime _addtime;


        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string fodder_type
        {
            get { return _fodder_type; }
            set { _fodder_type = value; }
        }
        public string fodder_xml
        {
            get { return _fodder_xml; }
            set { _fodder_xml = value; }
        }
        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
    }
}
