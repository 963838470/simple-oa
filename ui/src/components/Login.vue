<template>
  <div class="container">
    <div class="wrap" style="height:450px;">
      <span class="title">SimpleOA系统</span>
      <div class="main_content">
        <p class="">用户登录</p>
        <form>
          <div class="form-group mg-t20">
            <i class="icon-user icon_font"></i>
            <input type="email" class="login_input" id="Email1" placeholder="请输入用户名">
          </div>
          <div class="form-group mg-t20">
            <i class="icon-lock icon_font"></i>
            <input type="password" class="login_input" id="Password1" placeholder="请输入密码">
          </div>
          <div class="checkbox mg-b25">
            <label>
              <input type="checkbox">记住我的登录信息
            </label>
          </div>
          <button style="submit" class="login-btn">登 录</button>
        </form>
      </div>
      <!--row end-->
    </div>
    <!--container end-->
  </div>
</template>

<script>
export default {
  data() {
    return {
      msg: "Welcome to Your Vue.js App"
    };
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.container {
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background: url("../image/bg.jpg");
}

.wrap {
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  width: 600px;
  height: 450px;
  margin: auto;
  font-family: "微软雅黑";
  color: white;
}

.title {
  line-height: 40px;
  font-size: 30px;
  font-weight: 500;
  margin: 20px 10px 10px;
}

.main_content {
  width: 100%;
  height: 410px;
  background: url("../image/main_bg.png") repeat;
  border-radius: 8px;
}

.login_input {
  border: 1px solid #3872f6;
  border-radius: 3px;
  line-height: 40px;
  padding: 2px 5px 2px 30px;
  background: none;
  width: 95%;
  margin: 0 auto;
}

.login-btn {
  display: block;
  background: #3872f6;
  color: #fff;
  font-size: 15px;
  line-height: 50px;
  border-radius: 3px;
  border: none;
  width: 95%;
  margin: 0 auto;
}
</style>
