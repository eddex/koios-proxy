const express = require("express");
const { createProxyMiddleware } = require("http-proxy-middleware");

const app = express();

app.use(
  "/*",
  createProxyMiddleware({
    target: "https://api.koios.rest/",
    changeOrigin: true,
    onProxyRes: (proxyRes, req, res) => {
      console.log(req.originalUrl, res.statusCode);
      if (res.statusCode >= 400) {
        console.error(res.statusMessage);
      }
    },
  })
);

app.listen(5001);
