/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./Components/**/*.{razor,css,js}"],
  theme: {
      extend: {
          screens: {
              xs: { max: '525px'},
              sm: { max: '768px' },
              md: { min: '769px', max: '1023px' },
              lg: { min: '1024px' },
          },
          colors: {
              "admin-default": "#7e6fff",
              "txt-default": "252a2b",
          }
      },
  },
  plugins: [],
}

