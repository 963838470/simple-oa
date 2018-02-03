<template>
  <div class="container">
    <div class="wrap">
      <p>
        <span class="title">SimpleOA系统</span>
      </p>
      <div class="main_content">
        <p class="">用户登录</p>
        <form>
          <p>
            <i class="icon-user icon_font"></i>
            <input type="email" class="login_input" id="Email1" placeholder="请输入用户名">
          </p>
          <p>
            <i class="icon-lock icon_font"></i>
            <input type="password" class="login_input" id="Password1" placeholder="请输入密码">
          </p>
          <label>
            <input type="checkbox">记住我的登录信息
          </label>
          <p>
            <button style="submit" class="login-btn">登 录</button>
          </p>
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
  width: 550px;
  height: 450px;
  margin: auto;
  font-family: "微软雅黑";
  font-size: 16px;
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
  padding: 20px;
}

.login_input {
  border: 1px solid #3872f6;
  border-radius: 3px;
  line-height: 40px;
  padding: 2px 5px 2px 30px;
  background: none;
  width: 100%;
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
  width: 100%;
  margin: 0 auto;
}

p {
  line-height: 60px;
  margin-bottom: 20px;
}
</style>
