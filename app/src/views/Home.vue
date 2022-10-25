<template>
  <div class="flex overflow-hidden justify-center h-screen">
    <div class="flex flex-col md:flex-row items-center justify-center w-full h-full">
        <router-link
          to="/logic"
          class="w-full h-full flex flex-col justify-center text-4xl uppercase font-semibold shadow-md hover:bg-white text-center text-gray-500  hover:text-blue-500"
          >
              Logic Propositions
          </router-link
        >
        <router-link
          to="/automata"
          class="w-full h-full flex flex-col justify-center text-4xl uppercase font-semibold  shadow-md text-center hover:bg-white text-gray-500 hover:text-blue-500"
          >
              Finite State Automata
          </router-link
        >
    </div>
  </div>
</template>
