/// </summary>
///  EmailComposer.cs
///  Cordova EmailComposer Plugin
///
///  Created by Sebastian Katzer (github.com/katzer) on 07/10/2013.
///  Copyright 2013 Sebastian Katzer. All rights reserved.
///  GPL v2 licensed
/// </summary>

using De.APPPlant.Cordova.Plugin.EmailComposer;
using Microsoft.Phone.Tasks;
using System;
using System.Linq;
using WPCordovaClassLib.Cordova;
using WPCordovaClassLib.Cordova.Commands;
using WPCordovaClassLib.Cordova.JSON;

namespace Cordova.Extension.Commands
{
    /// <summary>
    /// Implementes access to email composer task
    /// http://msdn.microsoft.com/en-us/library/windowsphone/develop/hh394003(v=vs.105).aspx
    /// </summary>
    public class EmailComposer : BaseCommand
    {
        /// <summary>
        /// Überprüft, ob Emails versendet werden können.
        /// </summary>
        public void isServiceAvailable(string jsonArgs)
        {
            DispatchCommandResult(new PluginResult(PluginResult.Status.OK, true));
        }

        /// <summary>
        /// Öffnet den Email-Kontroller mit vorausgefüllten Daten.
        /// </summary>
        public void open(string jsonArgs)
        {
            string[] args                = JsonHelper.Deserialize<string[]>(jsonArgs);
            EmailComposerOptions options = JsonHelper.Deserialize<EmailComposerOptions>(args[0]);
            EmailComposeTask draft       = GetDraftWithProperties(options);

            OpenDraft(draft);

            DispatchCommandResult();
        }

        /// </summary>
        /// Erstellt den Email-Composer und fügt die übergebenen Eigenschaften ein.
        /// </summary>
        private EmailComposeTask GetDraftWithProperties(EmailComposerOptions options)
        {
            EmailComposeTask draft = new EmailComposeTask();

            SetSubject(options.Subject, draft);
            SetBody(options.Body, options.IsHtml, draft);
            SetTo(options.To, draft);
            SetCc(options.Cc, draft);
            SetBcc(options.Bcc, draft);
            SetAttachments(options.Attachments, draft);

            return draft;
        }

        /// </summary>
        /// Zeigt den ViewController zum Versenden/Bearbeiten der Mail an.
        /// </summary>
        private void OpenDraft(EmailComposeTask draft)
        {
            draft.Show();
        }

        /// </summary>
        /// Setzt den Subject der Mail.
        /// </summary>
        private void SetSubject(string subject, EmailComposeTask draft)
        {
            draft.Subject = subject;
        }

        /// </summary>
        /// Setzt den Body der Mail.
        /// </summary>
        private void SetBody(string body, Boolean isHTML, EmailComposeTask draft)
        {
            draft.Body = body;
        }

        /// </summary>
        /// Setzt die Empfänger der Mail.
        /// </summary>
        private void SetTo(string[] recipients, EmailComposeTask draft)
        {
            if (recipients != null)
            {
                draft.To = string.Join(",", recipients);
            }
        }

        /// </summary>
        /// Setzt die CC-Empfänger der Mail.
        /// </summary>
        private void SetCc(string[] recipients, EmailComposeTask draft)
        {
            if (recipients != null)
            {
                draft.Cc = string.Join(",", recipients);
            }
        }

        /// </summary>
        /// Setzt die BCC-Empfänger der Mail.
        /// </summary>
        private void SetBcc(string[] recipients, EmailComposeTask draft)
        {
            if (recipients != null)
            {
                draft.Bcc = string.Join(",", recipients);
            }
        }

        /// </summary>
        /// Fügt die Anhände zur Mail hinzu.
        /// </summary>
        private void SetAttachments(string[] attachments, EmailComposeTask draft)
        {
            // Not supported yet :(
        }
    }
}
