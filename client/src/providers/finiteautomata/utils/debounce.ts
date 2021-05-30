export type Procedure = (...args: any[]) => void;

export function debounce<F extends Procedure>(
  func: F,
  waitMilliseconds = 50,
  isImmediate = false
): (this: ThisParameterType<F>, ...args: Parameters<F>) => void {
  let timeoutId: ReturnType<typeof setTimeout> | undefined;

  return function (this: ThisParameterType<F>, ...args: Parameters<F>) {
    // eslint-disable-next-line @typescript-eslint/no-this-alias
    const context = this;

    const doLater = function () {
      timeoutId = undefined;
      if (!isImmediate) {
        func.apply(context, args);
      }
    };

    const shouldCallNow = isImmediate && timeoutId === undefined;

    if (timeoutId !== undefined) {
      clearTimeout(timeoutId);
    }

    timeoutId = setTimeout(doLater, waitMilliseconds);

    if (shouldCallNow) {
      func.apply(context, args);
    }
  };
}

export default debounce;
