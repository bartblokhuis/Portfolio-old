using Portfolio.Core.Interfaces.Common;
using Portfolio.Core.Services.QueuedEmails;
using Portfolio.Core.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Common
{
    public class QueuedMessagesSendTask : IScheduleTask
    {
        #region Fields

        private readonly IQueuedEmailService _queuedEmailService;
        private readonly IEmailService _emailService;

        #endregion

        #region Constructors

        public QueuedMessagesSendTask(IQueuedEmailService queuedEmailService, IEmailService emailService)
        {
            _queuedEmailService = queuedEmailService;
            _emailService = emailService;
        }

        #endregion

        #region Methods

        public async Task ExecuteAsync()
        {
            var maxTries = 3;
            var queuedEmails = await _queuedEmailService.SearcAsync(null, null, null, null,
                true, true, maxTries, false, 0, 500);

            foreach (var queuedEmail in queuedEmails)
            {
                try
                {
                    await _emailService.SendEmail(
                        queuedEmail.ToName,
                        queuedEmail.To,
                        queuedEmail.Subject,
                        queuedEmail.Body
                        );

                    queuedEmail.SentOnUtc = DateTime.UtcNow;
                }
                catch (Exception exc)
                {
                    //TODO Error logging
                }
                finally
                {
                    queuedEmail.SentTries += 1;
                    await _queuedEmailService.UpdateAsync(queuedEmail);
                }
            }
        }

        #endregion
    }
}
