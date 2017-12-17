using EIS.AppBase;
using EIS.AppBase.Config;
using NLog.LogReceiverService;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Web.UI;
using System.Text;

namespace EIS.Web.SysFolder.Permission
{
    public partial class LdapTree : AdminPageBase
    {
        public string treedata = "";

        private DirectoryEntry directoryEntry_0 = null;

        public string selmethod = "";

        public string string_0 = "";

        public string queryfield = "";


        private void method_0(TreeItem treeItem_0, string string_1, string string_2, string string_3)
        {
            this.directoryEntry_0 = new DirectoryEntry(string_1, string_2, string_3);
            if (this.directoryEntry_0 != null)
            {
                TreeItem treeItem = new TreeItem();
                Guid guid = new Guid((byte[])this.directoryEntry_0.Properties["objectGUID"].Value);
                treeItem.id = guid.ToString();
                treeItem.text = this.directoryEntry_0.Name.Substring(3);
                treeItem.@value = this.directoryEntry_0.Properties["distinguishedName"].Value.ToString();
                treeItem_0.Add(treeItem);
                try
                {
                    foreach (DirectoryEntry child in this.directoryEntry_0.Children)
                    {
                        if (!this.method_1(child))
                        {
                            continue;
                        }
                        this.method_0(treeItem, string.Concat("LDAP://", child.Properties["distinguishedName"].Value.ToString()), string_2, string_3);
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        private bool method_1(DirectoryEntry directoryEntry_1)
        {
            bool flag;
            StringCollection stringCollection = new StringCollection()
            {
                "organizationalUnit",
                "group"
            };
            if (directoryEntry_1.Name.ToLower() != "ou=domain controllers")
            {
                foreach (object item in directoryEntry_1.Properties["objectClass"])
                {
                    if (!stringCollection.Contains(item.ToString()))
                    {
                        continue;
                    }
                    flag = true;
                    return flag;
                }
                flag = false;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private void method_2()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs eventArgs_0)
        {
            this.method_2();
            base.OnInit(eventArgs_0);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TreeItem treeItem = new TreeItem()
            {
                id = "",
                text = "活动目录",
                @value = "",
                isexpand = true
            };
            LDAPConfig ldapConfig = AppSettings.Instance.LdapConfig;
            string[] serverIP = new string[] { "LDAP://", ldapConfig.ServerIP, ":", ldapConfig.ServerPort, "/", ldapConfig.RootPath };
            string str = string.Concat(serverIP);
            this.method_0(treeItem, str, ldapConfig.Account, ldapConfig.PassWord);
            this.treedata = treeItem.ToJsonString();
        }
    }
}