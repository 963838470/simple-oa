<template>
  <div>
    <el-row>
      <el-col :span="6">
        <!-- 通知 -->
        <el-card class="notice">
          <div slot="header" class="clearfix">
            <h3>通知</h3>
            <h4>更多>></h4>
          </div>
          <ul>
            <li v-for="n in notice" :key="n.id">
              <a href="javascript:void(0)" @click="showNoticeDetail(n)">{{ n.title }}</a>
              <div style="float:right">{{ n.createTime | date }}</div>
            </li>
          </ul>
        </el-card>
      </el-col>
      <el-col :span="18">
        <div class="grid-content bg-purple-light">
        </div>
      </el-col>
    </el-row>
    <!-- 通知详情 -->
    <el-dialog title="通知详情" :visible.sync="isShowNoticeDetail" :before-close="hideNoticeDetail">
      <el-form ref="form" :model="currentNote" label-width="80px">
        <el-form-item label="标题">
          <el-input v-model="currentNote.title" readonly></el-input>
        </el-form-item>
        <el-form-item label="内容">
          <el-input type="textarea" autosize v-model="currentNote.content" readonly></el-input>
        </el-form-item>
        <el-form-item label="创建时间">
          <el-input v-model="currentNote.createTime" readonly></el-input>
        </el-form-item>
        <el-form-item label="到期时间">
          <el-input v-model="currentNote.finishTime" readonly></el-input>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button type="primary" @click="hideNoticeDetail">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import data from "../../common/data.js";
export default {
  data() {
    return {
      notice: [],
      isShowNoticeDetail: false,
      currentNote: {}
    };
  },
  methods: {
    showNoticeDetail: function(note) {
      this.isShowNoticeDetail = true;
      this.currentNote = note;
    },
    hideNoticeDetail: function() {
      this.isShowNoticeDetail = false;
    }
  },
  mounted() {
    this.notice = data.notice;
  }
};
</script>

<style scoped>
.notice {
  text-align: left;
  margin-top: 10px;
}
h3 {
  float: left;
}

h4 {
  float: right;
}

.notice ul {
}

.notice ul li {
  list-style: none;
  margin-bottom: 15px;
}

a {
  text-decoration: none;
  color: black;
}
</style>
