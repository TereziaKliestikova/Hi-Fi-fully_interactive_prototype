// https://angular.io/guide/build
export const environment = {
  apiUrl:
    window['env']?.apiUrl !== '$$API_URL$$'
      ? window['env']?.apiUrl
      : 'http://localhost:81',
  upload: '/admin/upload-sample-image',
};
