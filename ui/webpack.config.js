const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const webpack = require('webpack');
require("babel-polyfill");

module.exports = {
    entry: {
        app: ["babel-polyfill", './src/index.js',] // 增加浏览器兼容性
    },
    output: {
        filename: 'bundle-[hash].js',
        path: path.resolve(__dirname, 'dist')
    },
    module: {
        rules: [
            { test: /\.vue$/, loader: ['vue-loader'], exclude: /node_modules/ },
            { test: /\.js$/, loader: "babel-loader", exclude: /node_modules/ },
            { test: /\.css$/, use: ['style-loader', 'css-loader', 'postcss-loader'] },
            { test: /\.(png|svg|jpg|gif)$/, use: ['file-loader'] },
            { test: /\.(woff|woff2|eot|ttf|otf)$/, use: ['file-loader'] },
        ]
    },
    devtool: 'inline-source-map', // source-map 报错时映射到错误的文件
    devServer: {
        contentBase: './dist',
        hot: true
    },
    plugins: [
        //new CleanWebpackPlugin(['dist']),   // 清空dist目录
        new HtmlWebpackPlugin({ template: 'src/index.html' }), // 生成新的index.html并包含引用
        new webpack.NamedModulesPlugin(),
        new webpack.HotModuleReplacementPlugin(),
        // new webpack.optimize.UglifyJsPlugin({
        //     beautify: false,
        //     comments: false,
        //     compress: {
        //         warnings: false,
        //         drop_console: true,
        //         collapse_vars: true,
        //         reduce_vars: true,
        //     }
        // }), // 压缩代码
    ],
};