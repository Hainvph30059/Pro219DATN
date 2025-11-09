/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./Components/**/*.{razor,css,js}"],
  theme: {
      extend: {
          screens: {
              sm: { max: '768px' },
              md: { min: '769px', max: '1023px' },
              lg: { min: '1024px' },
          },
      },
  },
  plugins: [],
}

