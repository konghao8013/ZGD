using System;
using System.Collections.Generic;
using System.Text;

namespace ZGD.Model
{
   public class wechat_bind
    {
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _wechat_no;

        public string wechat_no
        {
            get { return _wechat_no; }
            set { _wechat_no = value; }
        }
        private string _wechat_name;

        public string wechat_name
        {
            get { return _wechat_name; }
            set { _wechat_name = value; }
        }
        private string _wechat_logo;

        public string wechat_logo
        {
            get { return _wechat_logo; }
            set { _wechat_logo = value; }
        }
        private string _wechat_account;

        public string wechat_account
        {
            get { return _wechat_account; }
            set { _wechat_account = value; }
        }
        private string _wechat_pwd;

        public string wechat_pwd
        {
            get { return _wechat_pwd; }
            set { _wechat_pwd = value; }
        }
        private string _appid;

        public string appid
        {
            get { return _appid; }
            set { _appid = value; }
        }
        private string _appsecret;

        public string appsecret
        {
            get { return _appsecret; }
            set { _appsecret = value; }
        }
        private string _token;

        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        private int _wechat_check;

        public int wechat_check
        {
            get { return _wechat_check; }
            set { _wechat_check = value; }
        }
        private DateTime _addtime;

        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        private string _wechat_url;

        public string Wechat_url
        {
            get { return _wechat_url; }
            set { _wechat_url = value; }
        }
    }
}
