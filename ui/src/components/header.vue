<template>
  <div class="header">
    <h1><router-link to="/">{{ name }}</router-link></h1>
  </div>
</template>

<script>
  export default {
    data () {
      return {
        name: 'EASY_OA系统'
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .header {
    background:#20a0ff;
    line-height:60px;
    height:60px;
  }

  h1 {
    float:left;
    margin-left:50px;
  }

  a {
    color: white;
    text-decoration: none;
  }
</style>
