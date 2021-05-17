module.exports = {
  purge: {
    // Change enabled to false for faster build time
    enabled:
      process.env.NODE_ENV === "production" ,
    content: [
      "./src/**/*.vue",
      "./src/**/*.pcss",
      "./src/**/*.css",
      "./src/**/*.sass",
      "./src/**/*.html",
      "./src/**/*.js",
      "./src/**/*.ts"
    ],
    options: {
      whitelist: [
        // "flex-shrink-0",
        // "w-16",
        // "h-16"
      ]
    }
  },
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      maxHeight: {
       '0': '0',
       '1/4': '25%',
       '1/2': '50%',
       '3/4': '75%',
       'full': '100%',
      }
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
