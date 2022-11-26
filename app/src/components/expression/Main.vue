<template>
  <div class="flex flex-row relative h-screen overflow-hidden p-10">
      <div class="flex flex-col items-start h-full w-1/4">
        <div class="mb-5 px-2 flex flex-col">
          <span class="text-gray-600 font-bold">See also:</span>
              <router-link to="/automata" class="p-2 font-semibold text-gray-600 hover:text-blue-500 underline">Finite State Automata Evaluation</router-link> 
        </div>
        <div
          class="w-full rounded-xl border border-gray-300 justify-between flex flex-col items-center py-3 px-2"
        >
          <div
            @click="SeeOverview"
            :class="[
              OverviewVisible
                ? 'bg-green-600 hover:bg-green-700 border border-green-500 text-white rounded-md shadow-xl'
                : 'text-gray-800 hover:bg-gray-200'
            ]"
            class="cursor-pointer rounded-md font-semibold w-full flex justify-between px-6 py-4"
          >
            <span class="flex items-center "
              ><svg
                class="h-6 w-6 fill-current mr-4"
                viewBox="0 0 16 16"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M5 11.5C5 11.3674 5.05268 11.2402 5.14645 11.1464C5.24021 11.0527 5.36739 11 5.5 11H14.5C14.6326 11 14.7598 11.0527 14.8536 11.1464C14.9473 11.2402 15 11.3674 15 11.5C15 11.6326 14.9473 11.7598 14.8536 11.8536C14.7598 11.9473 14.6326 12 14.5 12H5.5C5.36739 12 5.24021 11.9473 5.14645 11.8536C5.05268 11.7598 5 11.6326 5 11.5ZM5 7.5C5 7.36739 5.05268 7.24021 5.14645 7.14645C5.24021 7.05268 5.36739 7 5.5 7H14.5C14.6326 7 14.7598 7.05268 14.8536 7.14645C14.9473 7.24021 15 7.36739 15 7.5C15 7.63261 14.9473 7.75979 14.8536 7.85355C14.7598 7.94732 14.6326 8 14.5 8H5.5C5.36739 8 5.24021 7.94732 5.14645 7.85355C5.05268 7.75979 5 7.63261 5 7.5ZM5 3.5C5 3.36739 5.05268 3.24021 5.14645 3.14645C5.24021 3.05268 5.36739 3 5.5 3H14.5C14.6326 3 14.7598 3.05268 14.8536 3.14645C14.9473 3.24021 15 3.36739 15 3.5C15 3.63261 14.9473 3.75979 14.8536 3.85355C14.7598 3.94732 14.6326 4 14.5 4H5.5C5.36739 4 5.24021 3.94732 5.14645 3.85355C5.05268 3.75979 5 3.63261 5 3.5ZM2 4.5C2.26522 4.5 2.51957 4.39464 2.70711 4.20711C2.89464 4.01957 3 3.76522 3 3.5C3 3.23478 2.89464 2.98043 2.70711 2.79289C2.51957 2.60536 2.26522 2.5 2 2.5C1.73478 2.5 1.48043 2.60536 1.29289 2.79289C1.10536 2.98043 1 3.23478 1 3.5C1 3.76522 1.10536 4.01957 1.29289 4.20711C1.48043 4.39464 1.73478 4.5 2 4.5V4.5ZM2 8.5C2.26522 8.5 2.51957 8.39464 2.70711 8.20711C2.89464 8.01957 3 7.76522 3 7.5C3 7.23478 2.89464 6.98043 2.70711 6.79289C2.51957 6.60536 2.26522 6.5 2 6.5C1.73478 6.5 1.48043 6.60536 1.29289 6.79289C1.10536 6.98043 1 7.23478 1 7.5C1 7.76522 1.10536 8.01957 1.29289 8.20711C1.48043 8.39464 1.73478 8.5 2 8.5V8.5ZM2 12.5C2.26522 12.5 2.51957 12.3946 2.70711 12.2071C2.89464 12.0196 3 11.7652 3 11.5C3 11.2348 2.89464 10.9804 2.70711 10.7929C2.51957 10.6054 2.26522 10.5 2 10.5C1.73478 10.5 1.48043 10.6054 1.29289 10.7929C1.10536 10.9804 1 11.2348 1 11.5C1 11.7652 1.10536 12.0196 1.29289 12.2071C1.48043 12.3946 1.73478 12.5 2 12.5V12.5Z"
                />
              </svg>
              <span>OVERVIEW</span></span
            >
            <span
              ><svg
                class="w-6 h-6 fill-current"
                viewBox="0 0 16 16"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fill-rule="evenodd"
                  clip-rule="evenodd"
                  d="M1 8.00014C1 7.86753 1.05268 7.74036 1.14645 7.64659C1.24021 7.55282 1.36739 7.50014 1.5 7.50014H13.293L10.146 4.35414C10.0521 4.26026 9.99937 4.13292 9.99937 4.00014C9.99937 3.86737 10.0521 3.74003 10.146 3.64614C10.2399 3.55226 10.3672 3.49951 10.5 3.49951C10.6328 3.49951 10.7601 3.55226 10.854 3.64614L14.854 7.64614C14.9006 7.69259 14.9375 7.74776 14.9627 7.80851C14.9879 7.86925 15.0009 7.93438 15.0009 8.00014C15.0009 8.06591 14.9879 8.13103 14.9627 8.19178C14.9375 8.25252 14.9006 8.3077 14.854 8.35414L10.854 12.3541C10.7601 12.448 10.6328 12.5008 10.5 12.5008C10.3672 12.5008 10.2399 12.448 10.146 12.3541C10.0521 12.2603 9.99937 12.1329 9.99937 12.0001C9.99937 11.8674 10.0521 11.74 10.146 11.6461L13.293 8.50014H1.5C1.36739 8.50014 1.24021 8.44747 1.14645 8.3537C1.05268 8.25993 1 8.13275 1 8.00014V8.00014Z"
                /></svg
            ></span>
          </div>
          <div
            @click="SeeTruthTable"
            :class="[
              TruthTableVisible
                ? 'bg-green-600 hover:bg-green-700 border border-green-500 text-white rounded-md shadow-xl'
                : 'text-gray-800 hover:bg-gray-200'
            ]"
            class="cursor-pointer rounded-md font-semibold w-full flex justify-between px-6 py-4"
          >
            <span class="flex items-center"
              ><svg
                class="w-6 h-6 mr-4 fill-current"
                viewBox="0 0 26 27"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M1 13.4688L0 13V11L13 5L26 11V13L25 13.4688V21.8125C25 22.2083 24.862 22.5052 24.5859 22.7031C24.3099 22.901 23.9479 23 23.5 23C23.0521 23 22.6901 22.901 22.4141 22.7031C22.138 22.5052 22 22.2083 22 21.8125V14.8438L14.5 18.3125V25.8125C14.5 26.2083 14.362 26.5052 14.0859 26.7031C13.8099 26.901 13.4479 27 13 27C12.5521 27 12.1901 26.901 11.9141 26.7031C11.638 26.5052 11.5 26.2083 11.5 25.8125V18.3125L4 14.8438V21.8125C4 22.2083 3.86198 22.5052 3.58594 22.7031C3.3099 22.901 2.94792 23 2.5 23C2.05208 23 1.6901 22.901 1.41406 22.7031C1.13802 22.5052 1 22.2083 1 21.8125V13.4688ZM13 17L25 11.5L13 6L1 11.5L13 17Z"
                />
                <path
                  d="M14.2984 3.2284H16.158V9.23077H17.7725V0.189349C17.7725 0.0852071 17.6989 0 17.609 0H16.624C16.5484 0 16.4809 0.0615382 16.4666 0.149112C16.4135 0.435503 16.3256 0.676923 16.203 0.880474C16.0559 1.11953 15.8781 1.31124 15.6655 1.45799C15.455 1.60473 15.2159 1.70651 14.9503 1.76331C14.7377 1.80828 14.5211 1.83432 14.2963 1.83669C14.2064 1.83905 14.1349 1.92189 14.1349 2.02604V3.03905C14.1349 3.1432 14.2084 3.2284 14.2984 3.2284ZM19.8365 10.4852H13.624C13.5341 10.4852 13.4605 10.5704 13.4605 10.6746V11.8107C13.4605 11.9148 13.5341 12 13.624 12H19.8365C19.9264 12 20 11.9148 20 11.8107V10.6746C20 10.5704 19.9264 10.4852 19.8365 10.4852ZM11.1083 1.27101C10.7793 0.821302 10.4053 0.504142 9.98025 0.314793C9.54496 0.12071 9.07902 0.0236686 8.58242 0.0236686C8.09401 0.0236686 7.6342 0.12071 7.19891 0.314793C6.77384 0.504142 6.40191 0.821302 6.07902 1.26864C5.75409 1.72071 5.49251 2.32663 5.29632 3.09112C5.10014 3.86036 5 4.81894 5 5.96686C5 7.14793 5.10014 8.13018 5.29632 8.91124C5.49251 9.68757 5.75409 10.3006 6.07902 10.7503C6.40191 11.1976 6.77384 11.5148 7.19891 11.7041C7.6342 11.8982 8.09401 11.9953 8.58242 11.9953C9.08106 11.9953 9.547 11.8982 9.98025 11.7041C10.4053 11.5148 10.7813 11.1976 11.1083 10.7479C11.4373 10.2982 11.701 9.68521 11.8971 8.90888C12.0933 8.12781 12.1935 7.14556 12.1935 5.9645C12.1935 4.81893 12.0933 3.86036 11.8971 3.08876C11.703 2.32899 11.4394 1.72308 11.1083 1.27101V1.27101ZM10.5054 7.2284C10.485 7.73491 10.4196 8.22249 10.3113 8.69349C10.1989 9.18343 10.015 9.60473 9.75954 9.9574C9.48161 10.3432 9.08106 10.5325 8.58242 10.5325C8.09196 10.5325 7.69959 10.3408 7.42371 9.95503C7.17439 9.60473 6.99251 9.18343 6.88011 8.69349C6.7718 8.22249 6.7064 7.73255 6.68597 7.2284C6.66553 6.7361 6.65531 6.31716 6.65531 5.96923C6.65531 5.76095 6.65736 5.51006 6.66349 5.21657C6.66757 4.91598 6.68597 4.60592 6.71662 4.28876C6.74728 3.96686 6.79837 3.64734 6.87193 3.33018C6.94755 3.00355 7.05177 2.70769 7.18665 2.44497C7.32766 2.17041 7.50954 1.94556 7.73229 1.77515C7.96526 1.59527 8.24932 1.50769 8.58038 1.50769C8.9094 1.50769 9.1955 1.59527 9.43256 1.77278C9.6594 1.9432 9.84741 2.16568 9.99251 2.44024C10.1335 2.70533 10.2398 3.00355 10.3113 3.33254C10.3787 3.64497 10.4298 3.96213 10.4646 4.28166C10.5014 4.60118 10.5218 4.91124 10.5259 5.2142C10.53 5.50769 10.5341 5.75858 10.5341 5.96686C10.5361 6.31716 10.5259 6.7361 10.5054 7.2284Z"
                />
              </svg>
              <span>
                SEE TRUTH TABLE
              </span>
            </span>
            <span
              ><svg
                class="w-5 h-5 fill-current"
                viewBox="0 0 16 16"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fill-rule="evenodd"
                  clip-rule="evenodd"
                  d="M1 8.00014C1 7.86753 1.05268 7.74036 1.14645 7.64659C1.24021 7.55282 1.36739 7.50014 1.5 7.50014H13.293L10.146 4.35414C10.0521 4.26026 9.99937 4.13292 9.99937 4.00014C9.99937 3.86737 10.0521 3.74003 10.146 3.64614C10.2399 3.55226 10.3672 3.49951 10.5 3.49951C10.6328 3.49951 10.7601 3.55226 10.854 3.64614L14.854 7.64614C14.9006 7.69259 14.9375 7.74776 14.9627 7.80851C14.9879 7.86925 15.0009 7.93438 15.0009 8.00014C15.0009 8.06591 14.9879 8.13103 14.9627 8.19178C14.9375 8.25252 14.9006 8.3077 14.854 8.35414L10.854 12.3541C10.7601 12.448 10.6328 12.5008 10.5 12.5008C10.3672 12.5008 10.2399 12.448 10.146 12.3541C10.0521 12.2603 9.99937 12.1329 9.99937 12.0001C9.99937 11.8674 10.0521 11.74 10.146 11.6461L13.293 8.50014H1.5C1.36739 8.50014 1.24021 8.44747 1.14645 8.3537C1.05268 8.25993 1 8.13275 1 8.00014V8.00014Z"
                />
              </svg>
            </span>
          </div>
          <div
            @click="SeeGraph"
            :class="[
              GraphVisible
                ? 'bg-green-600 hover:bg-green-700 border border-green-500 text-white rounded-md shadow-xl'
                : 'text-gray-800 hover:bg-gray-200'
            ]"
            class="cursor-pointer font-semibold w-full flex justify-between px-6 py-4 mb-2"
          >
            <span class="flex items-center"
              ><svg
                class="h-6 w-6 mr-4 fill-current"
                viewBox="0 0 24 24"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M19.5 17C19.36 17 19.24 17 19.11 17.04L17.5 13.8C17.95 13.35 18.25 12.71 18.25 12C18.25 11.337 17.9866 10.7011 17.5178 10.2322C17.0489 9.76339 16.413 9.5 15.75 9.5C15.61 9.5 15.5 9.5 15.35 9.54L13.74 6.3C14.21 5.84 14.5 5.21 14.5 4.5C14.5 3.83696 14.2366 3.20107 13.7678 2.73223C13.2989 2.26339 12.663 2 12 2C11.337 2 10.7011 2.26339 10.2322 2.73223C9.76339 3.20107 9.5 3.83696 9.5 4.5C9.5 5.2 9.79 5.84 10.26 6.29L8.65 9.54C8.5 9.5 8.39 9.5 8.25 9.5C7.58696 9.5 6.95107 9.76339 6.48223 10.2322C6.01339 10.7011 5.75 11.337 5.75 12C5.75 12.71 6.04 13.34 6.5 13.79L4.89 17.04C4.76 17 4.64 17 4.5 17C3.83696 17 3.20107 17.2634 2.73223 17.7322C2.26339 18.2011 2 18.837 2 19.5C2 20.163 2.26339 20.7989 2.73223 21.2678C3.20107 21.7366 3.83696 22 4.5 22C4.8283 22 5.15339 21.9353 5.45671 21.8097C5.76002 21.6841 6.03562 21.4999 6.26777 21.2678C6.49991 21.0356 6.68406 20.76 6.8097 20.4567C6.93534 20.1534 7 19.8283 7 19.5C7 18.8 6.71 18.16 6.24 17.71L7.86 14.46C8 14.5 8.12 14.5 8.25 14.5C8.38 14.5 8.5 14.5 8.63 14.46L10.26 17.71C9.79 18.16 9.5 18.8 9.5 19.5C9.5 20.163 9.76339 20.7989 10.2322 21.2678C10.7011 21.7366 11.337 22 12 22C12.663 22 13.2989 21.7366 13.7678 21.2678C14.2366 20.7989 14.5 20.163 14.5 19.5C14.5 18.837 14.2366 18.2011 13.7678 17.7322C13.2989 17.2634 12.663 17 12 17C11.87 17 11.74 17 11.61 17.04L10 13.8C10.45 13.35 10.75 12.71 10.75 12C10.75 11.3 10.46 10.67 10 10.21L11.61 6.96C11.74 7 11.87 7 12 7C12.13 7 12.26 7 12.39 6.96L14 10.21C13.6438 10.5573 13.3992 11.0031 13.2978 11.4902C13.1963 11.9773 13.2425 12.4836 13.4304 12.9443C13.6184 13.405 13.9395 13.7991 14.3527 14.0763C14.7659 14.3534 15.2524 14.5009 15.75 14.5C15.88 14.5 16 14.5 16.13 14.46L17.76 17.71C17.29 18.16 17 18.8 17 19.5C17 20.163 17.2634 20.7989 17.7322 21.2678C18.2011 21.7366 18.837 22 19.5 22C20.163 22 20.7989 21.7366 21.2678 21.2678C21.7366 20.7989 22 20.163 22 19.5C22 18.837 21.7366 18.2011 21.2678 17.7322C20.7989 17.2634 20.163 17 19.5 17V17ZM4.5 20.5C3.95 20.5 3.5 20.05 3.5 19.5C3.5 18.95 3.95 18.5 4.5 18.5C5.05 18.5 5.5 18.95 5.5 19.5C5.5 20.05 5.05 20.5 4.5 20.5ZM13 19.5C13 20.05 12.55 20.5 12 20.5C11.45 20.5 11 20.05 11 19.5C11 18.95 11.45 18.5 12 18.5C12.55 18.5 13 18.95 13 19.5ZM7.25 12C7.25 11.45 7.7 11 8.25 11C8.8 11 9.25 11.45 9.25 12C9.25 12.55 8.8 13 8.25 13C7.7 13 7.25 12.55 7.25 12ZM11 4.5C11 3.95 11.45 3.5 12 3.5C12.55 3.5 13 3.95 13 4.5C13 5.05 12.55 5.5 12 5.5C11.45 5.5 11 5.05 11 4.5ZM14.75 12C14.75 11.45 15.2 11 15.75 11C16.3 11 16.75 11.45 16.75 12C16.75 12.55 16.3 13 15.75 13C15.2 13 14.75 12.55 14.75 12ZM19.5 20.5C18.95 20.5 18.5 20.05 18.5 19.5C18.5 18.95 18.95 18.5 19.5 18.5C20.05 18.5 20.5 18.95 20.5 19.5C20.5 20.05 20.05 20.5 19.5 20.5Z"
                />
              </svg>
              <span>SEE GRAPH</span></span
            >
            <span
              ><svg
                class="w-5 h-5 fill-current"
                viewBox="0 0 16 16"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  fill-rule="evenodd"
                  clip-rule="evenodd"
                  d="M1 8.00014C1 7.86753 1.05268 7.74036 1.14645 7.64659C1.24021 7.55282 1.36739 7.50014 1.5 7.50014H13.293L10.146 4.35414C10.0521 4.26026 9.99937 4.13292 9.99937 4.00014C9.99937 3.86737 10.0521 3.74003 10.146 3.64614C10.2399 3.55226 10.3672 3.49951 10.5 3.49951C10.6328 3.49951 10.7601 3.55226 10.854 3.64614L14.854 7.64614C14.9006 7.69259 14.9375 7.74776 14.9627 7.80851C14.9879 7.86925 15.0009 7.93438 15.0009 8.00014C15.0009 8.06591 14.9879 8.13103 14.9627 8.19178C14.9375 8.25252 14.9006 8.3077 14.854 8.35414L10.854 12.3541C10.7601 12.448 10.6328 12.5008 10.5 12.5008C10.3672 12.5008 10.2399 12.448 10.146 12.3541C10.0521 12.2603 9.99937 12.1329 9.99937 12.0001C9.99937 11.8674 10.0521 11.74 10.146 11.6461L13.293 8.50014H1.5C1.36739 8.50014 1.24021 8.44747 1.14645 8.3537C1.05268 8.25993 1 8.13275 1 8.00014V8.00014Z"
                /></svg
            ></span>
          </div>
        </div>
        <div
          v-if="TruthTableVisible && shouldShowTableOptions"
          class="w-full mt-12 rounded-xl border border-gray-300 justify-content-center flex flex-col items-center py-3 px-2"
        >
          <truth-table-options />
        </div>

      </div>

    <div class="w-3/4 ml-10 container flex h-full flex-col">
      <div class="flex items-center justify-between pb-4 px-8">
        <input
          type="text"
          @input="e => setExpression((e.target as HTMLInputElement)!.value)"
          :value="expression"
          class="flex mr-4 w-full h-10 focus:outline-none focus:ring focus:border-green-700 rounded-md shadow-lg px-2"
        />
        <button
          @click="evaluateExpression"
          class="relative hover:bg-gray-100 focus:outline-none focus:ring rounded shadow-lg px-4 py-2 bg-white text-green-700 font-semibold  uppercase text-white"
        >
          <span
            v-if="ShouldEvaluate"
            class="flex absolute h-3 w-3 top-0 right-0 -mt-1 -mr-1"
          >
            <span
              class="animate-ping absolute inline-flex h-full w-full rounded-full bg-blue-400 opacity-75"
            ></span>
            <span
              class="relative inline-flex rounded-full h-3 w-3 bg-blue-500"
            ></span>
          </span>
          evaluate
        </button>
      </div>
      <div
        class="bg-white shadow-xl relative w-full overflow-y-auto h-full max-h-3/4 rounded-xl my-6"
      >
        <loading-spinner v-if="IS_EVALUATING" />
        <component
          v-else
          :is="component"
          :TruthTable="evaluation.TruthTable"
          :SimplifiedTruthTable="evaluation.SimplifiedTruthTable"
          :EvalutionData="evaluation"
        />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import {
  defineComponent,
  computed,
  onBeforeMount,
  shallowRef,
  getCurrentInstance
} from "vue";
import LoadingSpinner from "./LoadingSpinner.vue";
import GraphNetwork from "./GraphNetwork.vue";
import Overview from "./Overview.vue";
import TruthTable from "./TruthTable.vue";
import { expressionProvider } from "../../providers/expression/expressionProvider";
import Graph from "../../providers/expression/ExpressionGraph";
import TruthTableOptions from "./TruthTableOptions.vue";
export default defineComponent({
  components: {
    LoadingSpinner,
    Overview,
    GraphNetwork,
    TruthTable,
    TruthTableOptions
  },
  setup(_, context) {
    const component = shallowRef({});
    const instance = getCurrentInstance();

    const GraphVisible = computed(() => component.value === GraphNetwork);
    const TruthTableVisible = computed(() => component.value === TruthTable);
    const OverviewVisible = computed(() => component.value === Overview);

    const {
      IS_EVALUATING,
      expression,
      ShouldEvaluate,
      evaluation,
      evaluate,
      setExpression
    } = expressionProvider();

    const shouldShowTableOptions = computed(
      () => Object.keys(evaluation.value.TableToShow.value).length > 0
    );
    const graph = computed(() => Graph(evaluation.value));

    const SeeGraph = () => {
      component.value = GraphNetwork;
      instance?.proxy?.$nextTick(() => graph.value.showGraph());
    };
    const SeeOverview = () => (component.value = Overview);
    const SeeTruthTable = () => (component.value = TruthTable);

    const evaluateExpression = () => {
      evaluate()
        .then(() => component.value === GraphNetwork && SeeGraph())
        .catch(e => console.log("Couldn't evaluate expression!"));
    };

    onBeforeMount(() => {
      component.value = Overview;
      evaluateExpression();
    });

    return {
      component,
      SeeGraph,
      GraphVisible,
      TruthTableVisible,
      SeeTruthTable,
      SeeOverview,
      Overview,
      OverviewVisible,
      IS_EVALUATING,
      expression,
      evaluation,
      evaluateExpression,
      setExpression,
      ShouldEvaluate,
      shouldShowTableOptions
    };
  }
});
</script>
<style scoped>
/* width */
::-webkit-scrollbar {
  width: 10px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #ebebeb;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #c9c9c9;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: rgb(179, 179, 179);
}
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s;
}
.fade-enter, .fade-leave-to /* .fade-leave-active below version 2.1.8 */ {
  opacity: 0;
}
</style>
