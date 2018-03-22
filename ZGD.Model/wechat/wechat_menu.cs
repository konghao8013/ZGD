using System;
using System.Collections.Generic;
using System.Text;

namespace ZGD.Model
{
    public class wechat_menu
    {
        public int id;
        public string menu_name="";
        public string menu_key = "";
        public int menu_parent_id;
        public int menu_sort;
        public int menu_content_id;
        public string menu_type = "";
        public string menu_url = "";
        public int menu_is_show;
        public DateTime addtime;
    }
}
