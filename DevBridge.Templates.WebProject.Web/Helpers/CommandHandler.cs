﻿using System;
using System.Text;

using Common.Logging;

using DevBridge.Templates.WebProject.DataContracts.Exceptions;
using DevBridge.Templates.WebProject.Tools.Commands;
using DevBridge.Templates.WebProject.Tools.Messages;
using DevBridge.Templates.WebProject.Web.Resources;

namespace DevBridge.Templates.WebProject.Web.Helpers
{
    /// <summary>
    /// Contains logic to handle command execution in the controller context.
    /// </summary>
    public static class CommandHandler
    {
        /// <summary>
        /// Current class logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="command">The command.</param>
        public static void ExecuteCommand(this ICommand command)
        {
            try
            {
                command.Execute();
            }
            catch (MessagePropagationException ex)
            {
                HandleValidationException(ex, command);
            }
            catch (ConcurrentDataException ex)
            {
                HandleConcurrentDataException(ex, command);
            }
            catch (KnownException ex)
            {
                HandleCmsException(ex, command);
            }
            catch (Exception ex)
            {
                HandleException(ex, command);
            }
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <typeparam name="TRequest">The type of the command request.</typeparam>
        /// <param name="command">The command.</param>
        /// <param name="request">The request.</param>
        public static void ExecuteCommand<TRequest>(this ICommand<TRequest> command, TRequest request)
        {
            try
            {
                command.Execute(request);
            }
            catch (MessagePropagationException ex)
            {
                HandleValidationException(ex, command, request);
            }
            catch (ConcurrentDataException ex)
            {
                HandleConcurrentDataException(ex, command);
            }
            catch (KnownException ex)
            {
                HandleCmsException(ex, command, request);
            }
            catch (Exception ex)
            {
                HandleException(ex, command, request);
            }
        }

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <typeparam name="TRequest">The type of the command request.</typeparam>
        /// <typeparam name="TResponse">The type of the command response.</typeparam>
        /// <param name="command">The command.</param>
        /// <param name="request">The request.</param>
        /// <returns>Command result or default(TResponse) value if exception occurred.</returns>
        public static TResponse ExecuteCommand<TRequest, TResponse>(this ICommand<TRequest, TResponse> command, TRequest request)
        {
            try
            {
                return command.Execute(request);
            }
            catch (MessagePropagationException ex)
            {
                HandleValidationException(ex, command, request);
            }
            catch (ConcurrentDataException ex)
            {
                HandleConcurrentDataException(ex, command);
            }
            catch (KnownException ex)
            {
                HandleCmsException(ex, command, request);
            }
            catch (Exception ex)
            {
                HandleException(ex, command, request);
            }

            return default(TResponse);
        }

        /// <summary>
        /// Handles the validation exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <param name="command">The command.</param>
        /// <param name="request">The request.</param>
        private static void HandleValidationException(MessagePropagationException ex, ICommandBase command, object request = null)
        {
            Log.Trace(FormatCommandExceptionMessage(command, request), ex);

            string message = null;
            if (ex.Resource != null)
            {
                message = ex.Resource(ex);
            }

            if (string.IsNullOrEmpty(message))
            {
                message = Global.Message_InternalServerErrorPleaseRetry;
            }

            if (command.Context != null)
            {
                command.Context.Messages.AddError(message);
            }
        }

        /// <summary>
        /// Handles the concurrent data exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <param name="command">The command.</param>
        /// <param name="request">The request.</param>
        private static void HandleConcurrentDataException(ConcurrentDataException ex, ICommandBase command, object request = null)
        {
            Log.Trace(FormatCommandExceptionMessage(command, request), ex);

            string message = Global.Message_ConcurentDataException;

            if (command.Context != null)
            {
                command.Context.Messages.AddError(message);
            }
        }

        /// <summary>
        /// Handles the CMS exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <param name="command">The command.</param>
        /// <param name="request">The request.</param>
        private static void HandleCmsException(KnownException ex, ICommandBase command, object request = null)
        {
            Log.Error(FormatCommandExceptionMessage(command, request), ex);
            if (command.Context != null)
            {
                command.Context.Messages.AddError(Global.Message_InternalServerErrorPleaseRetry);
            }
        }

        /// <summary>
        /// Handles the unknown exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <param name="command">The command.</param>
        /// <param name="request">The request.</param>
        private static void HandleException(Exception ex, ICommandBase command, object request = null)
        {
            Log.Fatal(FormatCommandExceptionMessage(command, request), ex);
            if (command.Context != null)
            {
                command.Context.Messages.AddError(Global.Message_InternalServerErrorPleaseRetry);
            }
        }

        /// <summary>
        /// Creates command exception message.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="request">The request.</param>
        /// <returns>Command execution failure message.</returns>
        private static string FormatCommandExceptionMessage(object command, object request = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Failed to execute command {0}. ", command != null ? command.GetType().Name : "NULL");

            if (request != null)
            {
                sb.AppendFormat("Request data: {0}. ", request);
            }

            return sb.ToString();
        }
    }
}