<template>
  <div class="flex flex-col overflow-hidden justify-center h-screen p-10">
    <div class="flex flex-col md:flex-row items-center justify-center ">
      <div class="p-32">
        <router-link
          to="/ale1"
          class="p-20 lg:p-40 rounded-2xl shadow-md border text-center text-4xl uppercase text-gray-500 font-semibold
          transform duration-500 hover:shadow-lg hover:scale-125 hover:text-blue-500"
          >ALE1</router-link
        >
      </div>
      <div class="lg:p-32">
        <router-link
          to="/ale2"
          class="p-20 lg:p-40 rounded-2xl shadow-md border text-center text-4xl uppercase text-gray-500 font-semibold
           transform duration-500 hover:shadow-lg hover:scale-125 hover:text-blue-500"
          >ALE2</router-link
        >
      </div>
    </div>
  </div>
</template>
