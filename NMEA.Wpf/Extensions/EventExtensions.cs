using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMEA.Wpf.Common.Events;
using Prism.Events;

namespace NMEA.Wpf.Extensions
{
    public static class EventExtensions
    {
        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="model"></param>
        public static void Message(this IEventAggregator aggregator, MessageModel model)
        {
            aggregator.GetEvent<MessageEvent>().Publish(model);
        }

        /// <summary>
        /// 注册消息事件
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void Register(this IEventAggregator aggregator, Action<MessageModel> action)
        {
            aggregator.GetEvent<MessageEvent>().Subscribe(action);
        }

        /// <summary>
        /// 推送等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="model"></param>
        public static void UpdateLoading(this IEventAggregator aggregator, LoadingModel model)
        {
            aggregator.GetEvent<LoadingEvent>().Publish(model);
        }

        /// <summary>
        /// 注册等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void Register(this IEventAggregator aggregator, Action<LoadingModel> action)
        {
            aggregator.GetEvent<LoadingEvent>().Subscribe(action);
        }

        public static void Register(this IEventAggregator aggregator, Action<GsvModel> action)
        {
            aggregator.GetEvent<GsvEvent>().Subscribe(action);
        }

        public static void SetGsv(this IEventAggregator aggregator, GsvModel model)
        {
            aggregator.GetEvent<GsvEvent>().Publish(model);
        }

        public static void Register(this IEventAggregator aggregator, Action<SerialMessageModel> action)
        {
            aggregator.GetEvent<SerialMessageEvent>().Subscribe(action);
        }

        public static void SetSerialMessage(this IEventAggregator aggregator, SerialMessageModel model)
        {
            aggregator.GetEvent<SerialMessageEvent>().Publish(model);
        }
    }
}
