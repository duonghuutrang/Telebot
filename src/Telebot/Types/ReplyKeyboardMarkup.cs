﻿namespace Taikandi.Telebot.Types
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// This object represents a custom keyboard with reply options.
    /// See Introduction to bots for details and examples at https://core.telegram.org/bots#keyboards.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ReplyKeyboardMarkup : IReply
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplyKeyboardMarkup"/> class.
        /// </summary>
        /// <param name="oneTimeKeyboard">
        /// If set to <c>true</c> requests clients to hide the keyboard as soon as it's been used. Defaults to <c>false</c>.
        /// </param>
        /// <param name="selective">
        /// If set to <c>true</c> hides custom keyboard for specific users only. Defaults to <c>true</c>.
        /// </param>
        public ReplyKeyboardMarkup(bool oneTimeKeyboard = false, bool selective = true)
        {
            this.OneTimeKeyboard = oneTimeKeyboard;
            this.Selective = selective;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets an array of button rows, each represented by an array of strings
        /// </summary>
        [JsonProperty("keyboard", Required = Required.Always)]
        public string[][] Keyboard { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to requests clients to hide the keyboard
        /// as soon as it's been used. Defaults to <c>false</c>.
        /// </summary>
        [JsonProperty("one_time_keyboard")]
        public bool OneTimeKeyboard { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to Requests clients to resize the
        /// keyboard vertically for optimal fit (e.g., make the keyboard smaller if
        /// there are just two rows of buttons).
        /// Defaults to <c>false</c>, in which case the custom keyboard is always of
        /// the same height as the app's standard keyboard.
        /// </summary>
        [JsonProperty("resize_keyboard")]
        public bool ResizeKeyboard { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether you want to show the keyboard to specific users only.
        /// <para>
        /// Targets: 1) users that are @mentioned in the text of the Message object;
        /// 2) if the bot's message is a reply (has reply_to_message_id), sender of the original message.
        /// Example: A user requests to change the bot‘s language, bot replies to the request with a keyboard to
        /// select the new language. Other users in the group don’t see the keyboard.
        /// </para>
        /// </summary>
        [JsonProperty("selective")]
        public bool Selective { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a <see cref="ReplyKeyboardMarkup"/> filled with the specified <paramref name="keyboardTitles"/>.
        /// </summary>
        /// <param name="keyboardTitles">
        /// The titles of each keyboard button.
        /// </param>
        /// <param name="oneTimeKeyboard">
        /// If set to <c>true</c> requests clients to hide the keyboard as soon as it's been used. Defaults to <c>true</c>.
        /// </param>
        /// <param name="selective">
        /// If set to <c>true</c> hides custom keyboard for specific users only. Defaults to <c>true</c>.
        /// </param>
        /// <returns>
        /// A <see cref="ReplyKeyboardMarkup"/> filled with the specified <paramref name="keyboardTitles"/>.
        /// </returns>
        public static ReplyKeyboardMarkup CreateReplyKeyboardMarkup(ICollection<string> keyboardTitles, bool oneTimeKeyboard = true, bool selective = true)
        {
            var markup = new ReplyKeyboardMarkup(oneTimeKeyboard, selective);
            var totalRows = (int)Math.Ceiling(keyboardTitles.Count / 2d);
            markup.Keyboard = new string[totalRows][];

            var i = 0;
            foreach( var title in keyboardTitles )
            {
                var index = (int)(i / 2f);
                if( i % 2 != 0 )
                    markup.Keyboard[index][1] = title;
                else
                {
                    markup.Keyboard[index] = new string[2];
                    markup.Keyboard[index][0] = title;
                }

                i++;
            }

            return markup;
        }

        #endregion
    }
}