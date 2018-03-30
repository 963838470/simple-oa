import Vue from "vue"

Vue.filter("filterSex", function(value) {
  var sexObj = { 0: "男", 1: "女" }
  return sexObj[value]
})

Vue.filter("date", function(value) {
  var format = "yyyy-MM-dd"
  var date = new Date(value.replace(/-/g, "/").replace("T", " ")) // 将日期字符串转化为yyyy/MM/dd HH:mm:ss格式，兼容大部分浏览器
  if (/(y+)/.test(format)) {
    format = format.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length))
  }
  let o = {
    "M+": date.getMonth() + 1,
    "d+": date.getDate(),
    "h+": date.getHours(),
    "m+": date.getMinutes(),
    "s+": date.getSeconds()
  }
  for (let k in o) {
    let str = o[k] + ""
    if (new RegExp(`(${k})`).test(format)) {
      format = format.replace(RegExp.$1, RegExp.$1.length === 1 ? str : ("00" + str).substr(str.length))
    }
  }
  return format
})

Vue.filter("time", function(value, format) {
  if (format == null) {
    format = "yyyy-MM-dd hh:mm:ss"
  }
  var date = new Date(value.replace(/-/g, "/").replace("T", " ")) // 将日期字符串转化为yyyy/MM/dd HH:mm:ss格式，兼容大部分浏览器
  if (/(y+)/.test(format)) {
    format = format.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length))
  }
  let o = {
    "M+": date.getMonth() + 1,
    "d+": date.getDate(),
    "h+": date.getHours(),
    "m+": date.getMinutes(),
    "s+": date.getSeconds()
  }
  for (let k in o) {
    let str = o[k] + ""
    if (new RegExp(`(${k})`).test(format)) {
      format = format.replace(RegExp.$1, RegExp.$1.length === 1 ? str : ("00" + str).substr(str.length))
    }
  }
  return format
})
