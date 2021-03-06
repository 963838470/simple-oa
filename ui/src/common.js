import { Notification, confirm } from 'element-ui';

export default {
  // 机构
  success: function (msg) {
    Notification.success({
      title: '成功',
      message: msg
    });
  },
  warning: function (msg) {
    Notification.warning({
      title: '警告',
      message: msg
    });
  },
  info: function (msg) {
    Notification.info({
      title: '消息',
      message: msg
    });
  },
  error: function (msg) {
    Notification.error({
      title: '错误',
      message: msg
    });
  },
  confirm(msg) {
    confirm(msg, "提示", {
      type: "warning"
    })
  },
  getRandom: function () {
    return parseInt(Math.random() * 10000000) + 1;;
  },
  convertIntArray: function (str) {
    var array = [];
    var strArray = str.split(",");
    for (var i = 0; i < strArray.length; i++) {
      if (strArray[i] != null && strArray[i] != "" && !isNaN(parseInt(strArray[i]))) {
        array.push(parseInt(strArray[i]));
      }
    }
    return array;
  }
}

/*
this.$confirm('此操作将永久删除该数据, 是否继续?', '提示', {
  type: 'warning'
}).then(() => {

}).catch(() => {

});



*/
