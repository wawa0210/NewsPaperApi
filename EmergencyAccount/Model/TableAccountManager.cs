using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmergencyAccount.Model
{

    [Table("T_Sys_Manager")]
    public class TableAccountManager
    {
        #region 私有成员
        //在这里设置字段的默认值
        private string _id;
        private int _roleId;
        private int _deptId;
        private string _userName = string.Empty;
        private string _userPwd = string.Empty;
        private string _userSalt = string.Empty;
        private string _realName = string.Empty;
        private string _tel = string.Empty;
        private int _isLock;
        private int _level;
        private DateTime? _addTime;
        #endregion

        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TableAccountManager()
        {
        }
        /// <summary>
        /// 全参数构造函数
        /// </summary>
        public TableAccountManager(string id, int roleId, int deptId, string userName, string userPwd, string userSalt, string realName, string tel, int isLock, int level, DateTime? addTime)
        {
            _id = id;
            _roleId = roleId;
            _deptId = deptId;
            _userName = userName;
            _userPwd = userPwd;
            _userSalt = userSalt;
            _realName = realName;
            _tel = tel;
            _isLock = isLock;
            _level = level;
            _addTime = addTime;

        }
        #endregion

        #region 属性

        /// <summary>
        /// 属性: 
        /// </summary>
        [Column("Id")]
        [Key]
        [Description("")]
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// 属性: 角色编号
        /// </summary>
        [Column("RoleId")]
        [Description("角色编号")]
        public int RoleId
        {
            get => _roleId;
            set
            {
                _roleId = value;
            }
        }

        /// <summary>
        /// 属性: 部门编号
        /// </summary>
        [Column("DeptId")]
        [Description("部门编号")]
        public int DeptId
        {
            get => _deptId;
            set
            {
                _deptId = value;
            }
        }

        /// <summary>
        /// 属性: 用户名
        /// </summary>
        [Column("UserName")]
        [Description("用户名")]
        public string UserName
        {
            get => _userName == null ? string.Empty : _userName.Trim();
            set
            {
                _userName = value;
            }
        }

        /// <summary>
        /// 属性: 用户密码
        /// </summary>
        [Column("UserPwd")]
        [Description("用户密码")]
        public string UserPwd
        {
            get => _userPwd == null ? string.Empty : _userPwd.Trim();
            set
            {
                _userPwd = value;
            }
        }

        /// <summary>
        /// 属性: 密码盐
        /// </summary>
        [Column("UserSalt")]
        [Description("密码盐")]
        public string UserSalt
        {
            get => _userSalt == null ? string.Empty : _userSalt.Trim();
            set
            {
                _userSalt = value;
            }
        }

        /// <summary>
        /// 属性: 真实姓名
        /// </summary>
        [Column("RealName")]
        [Description("真实姓名")]
        public string RealName
        {
            get => _realName == null ? string.Empty : _realName.Trim();
            set
            {
                _realName = value;
            }
        }

        /// <summary>
        /// 属性: 电话号码
        /// </summary>
        [Column("Tel")]
        [Description("电话号码")]
        public string Tel
        {
            get => _tel == null ? string.Empty : _tel.Trim();
            set
            {
                _tel = value;
            }
        }

        /// <summary>
        /// 属性: 是否可用
        /// </summary>
        [Column("IsLock")]
        [Description("是否可用")]
        public int IsLock
        {
            get => _isLock;
            set
            {
                _isLock = value;
            }
        }

        /// <summary>
        /// 属性: 是否可用
        /// </summary>
        [Column("Level")]
        [Description("是否可用")]
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
            }
        }

        /// <summary>
        /// 属性: 添加时间
        /// </summary>
        [Column("AddTime")]
        [Description("添加时间")]
        public DateTime? AddTime
        {
            get => _addTime;
            set
            {
                _addTime = value;
            }
        }
        #endregion
    }
}
