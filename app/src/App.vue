<template>
  <div id="app" class="bg-gray-100">
    <router-view v-slot="{ Component }">
      <keep-alive>
        <component :is="Component" />
      </keep-alive>
  </router-view>
  </div>
</template>