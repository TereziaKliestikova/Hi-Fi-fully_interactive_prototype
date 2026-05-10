#!/bin/sh

if [ -n "${API_URL}" ]; then
  echo "API_URL is set to ${API_URL}. Using this value."
  sed -i 's|\$\$API_URL\$\$'"|${API_URL}"'|g' /usr/share/nginx/html/index.html
fi

nginx -g "daemon off;"
