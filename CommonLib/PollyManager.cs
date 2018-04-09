using Polly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public static class PollyManager
    {
        /// <summary>
        /// 带返回值重试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="continueOnCapturedContext"></param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public static async Task<T> ProcessAsync<T>(Func<Task<T>> func, bool continueOnCapturedContext = false, int retryCount = 10)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .RetryAsync(retryCount);

                return await retryTimesPolicy.ExecuteAsync(async () => await func(), continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 带返回值重试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="action">错误记录</param>
        /// <param name="continueOnCapturedContext"></param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public static async Task<T> ProcessAsync<T>(Func<Task<T>> func, Action<Exception> action, bool continueOnCapturedContext = false, int retryCount = 10)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .RetryAsync(retryCount, (ex, count) =>
                      {
                          action(ex);
                      });

                return await retryTimesPolicy.ExecuteAsync(async () => await func(), continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 带返回值重试,延时执行 1,2,4,8,16
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="continueOnCapturedContext"></param>
        /// <param name="retryCount"></param>
        /// <param name="powerVal">延时基础值</param>
        /// <returns></returns>
        public static async Task<T> ProcessAsync<T>(Func<Task<T>> func, bool continueOnCapturedContext = false, int retryCount = 10, int powerVal = 2)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(powerVal, retryAttempt)));

                return await retryTimesPolicy.ExecuteAsync(async () => await func(), continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 带返回值重试,延时执行 1,2,4,8,16
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="action">异常记录方法</param>
        /// <param name="continueOnCapturedContext"></param>
        /// <param name="retryCount"></param>
        /// <param name="powerVal"></param>
        /// <returns></returns>
        public static async Task<T> ProcessAsync<T>(Func<Task<T>> func, Action<Exception> action, bool continueOnCapturedContext = false, int retryCount = 10, int powerVal = 2)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(powerVal, retryAttempt)), (ex, timeSpan) =>
                      {
                          action(ex);
                      });

                return await retryTimesPolicy.ExecuteAsync(async () => await func(), continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="action">异常记录函数</param>
        /// <param name="continueOnCapturedContext"></param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public static async Task ProcessAsync(Func<Task> func, Action<Exception> action, bool continueOnCapturedContext = false, int retryCount = 10)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                       .RetryAsync(retryCount, (ex, count) =>
                       {
                           action(ex);
                       });

                await retryTimesPolicy.ExecuteAsync(async () =>
                {
                    await func();
                }, continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task ProcessAsync(Func<Task> func, bool continueOnCapturedContext = false, int retryCount = 10)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .RetryAsync(retryCount);

                await retryTimesPolicy.ExecuteAsync(async () =>
                {
                    await func();
                }, continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="action">异常记录函数</param>
        /// <param name="continueOnCapturedContext"></param>
        /// <param name="retryCount"></param>
        /// <param name="powerVal"></param>
        /// <returns></returns>
        public static async Task ProcessAsync(Func<Task> func, Action<Exception> action, bool continueOnCapturedContext = false, int retryCount = 10, int powerVal = 2)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(powerVal, retryAttempt)), (ex, timeSpan) =>
                      {
                          action(ex);
                      });

                await retryTimesPolicy.ExecuteAsync(async () =>
                {
                    await func();
                }, continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task ProcessAsync(Func<Task> func, bool continueOnCapturedContext = false, int retryCount = 10, int powerVal = 2)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(powerVal, retryAttempt)));

                await retryTimesPolicy.ExecuteAsync(async () =>
                {
                    await func();
                }, continueOnCapturedContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="action">异常记录函数</param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public static T Process<T>(Func<T> func, Action<Exception> action, int retryCount = 10)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                       .Retry(retryCount, (ex, count) =>
                       {
                           action(ex);
                       });

                return retryTimesPolicy.Execute(func);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T Process<T>(Func<T> func, int retryCount = 10)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .RetryAsync(retryCount);

                return retryTimesPolicy.Execute(() =>
                {
                    return func();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="action">异常记录函数</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="powerVal">加权数</param>
        /// <returns></returns>
        public static T Process<T>(Func<T> func, Action<Exception> action, int retryCount = 10, int powerVal = 2)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(powerVal, retryAttempt)), (ex, timeSpan) =>
                      {
                          action(ex);
                      });

                return retryTimesPolicy.Execute(() =>
                {
                    return func();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="powerVal">加权数</param>
        /// <returns></returns>
        public static T Process<T>(Func<T> func, int retryCount = 10, int powerVal = 2)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(powerVal, retryAttempt)));

                return retryTimesPolicy.Execute(func);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="actionLog">异常记录</param>
        /// <param name="retryCount">重试次数</param>
        public static void Process(Action action, Action<Exception> actionLog, int retryCount = 10)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                       .Retry(retryCount, (ex, count) =>
                       {
                           actionLog(ex);
                       });

                retryTimesPolicy.Execute(action);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Process(Action action, int retryCount = 10)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .Retry(retryCount);

                retryTimesPolicy.Execute(action);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="actionLog">异常记录</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="powerVal"></param>
        public static void Process(Action action, Action<Exception> actionLog, int retryCount = 10, int powerVal = 2)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(powerVal, retryAttempt)), (ex, timeSpan) =>
                      {
                          actionLog(ex);
                      });

                retryTimesPolicy.Execute(action);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="retryCount">测试次数</param>
        /// <param name="powerVal">加权数量</param>
        public static void Process(Action action, int retryCount = 10, int powerVal = 2)
        {
            try
            {
                var retryTimesPolicy =
                  Policy
                      .Handle<Exception>()
                      .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(powerVal, retryAttempt)));

                retryTimesPolicy.Execute(action);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
