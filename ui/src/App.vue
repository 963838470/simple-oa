<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  mounted() {
    this.$ajax.get("user").then(res => {
      var users = res.data;
      if (users.length > 0) {
        this.$store.commit("setUser", users[0]);
      }
    });
    this.$ajax.get("ou").then(res => {
      var ous = res.data;
      if (ous.length > 0) {
        this.$store.commit("setOu", ous[0]);
      }
    });
  }
};
</script>

<style>

</style>
