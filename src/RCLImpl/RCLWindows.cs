﻿using System;
using System.Runtime.InteropServices;
namespace rclcs
{
    internal class RCLWindows : RCLBase
    {
        /// <summary>
        /// This method does the initilisation of the ros client lib.
        /// <remarks>Call this method before you do any other calls to ros</remarks>
        /// </summary>
        /// <param name="args">Commandline arguments</param>
        /// <exception cref="RCLAlreadInitExcption">In case rcl was alread initialised</exception>
        public override rcl_context_t Init(String[] args)
        {
            if (args == null)
                throw new ArgumentNullException();

            rcl_init_options_t init_options = rcl_get_zero_initialized_init_options();
            var ret = rcl_init_options_init(ref init_options, Allocator.rcl_get_default_allocator());
            rcl_context_t context = rcl_get_zero_initialized_context();
            RCLReturnValues retVal = (RCLReturnValues)rcl_init(args.Length, args, ref init_options, ref context);

            switch (retVal)
            {
                case RCLReturnValues.RCL_RET_OK:

                    break;
                case RCLReturnValues.RCL_RET_ALREADY_INIT:
                    throw new RCLAlreadyInitExcption();
                case RCLReturnValues.RCL_RET_BAD_ALLOC:
                    throw new RCLBadAllocException();
                case RCLReturnValues.RCL_RET_ERROR:
                    throw new RCLErrorException(RCLErrorHandling.Instance.GetRMWErrorState());
                default:
                    break;
            }

            return context;
        }
        /// <summary>
        /// This method does the initilisation of the ros client lib
        /// <remarks>Call this method before you do any other calls to ros
        /// You can specify a custom memory allocator for ros but I wouldn't recommend doing this at the moment. </remarks>
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <param name="custom_allocator">Custom allocator.</param>
        public override rcl_context_t Init(String[] args, rcl_allocator_t custom_allocator)
        {
            if (args == null)
                throw new ArgumentNullException();

            rcl_init_options_t init_options = rcl_get_zero_initialized_init_options();
            var ret = rcl_init_options_init(ref init_options, custom_allocator);
            rcl_context_t context = rcl_get_zero_initialized_context();
            RCLReturnValues retVal = (RCLReturnValues)rcl_init(args.Length, args, ref init_options, ref context);

            switch (retVal)
            {
                case RCLReturnValues.RCL_RET_OK:

                    break;
                case RCLReturnValues.RCL_RET_ALREADY_INIT:
                    throw new RCLAlreadyInitExcption();
                case RCLReturnValues.RCL_RET_BAD_ALLOC:
                    throw new RCLBadAllocException();
                case RCLReturnValues.RCL_RET_ERROR:
                    throw new RCLErrorException(RCLErrorHandling.Instance.GetRMWErrorState());
                default:
                    break;
            }

            return context;
        }
        /// <summary>
        /// Gets a value indicating whether there was a Init call in the past.
        /// </summary>
        /// <value><c>true</c> if this instance is init; otherwise, <c>false</c>.</value>
        public override bool IsInit
        {
            get { return rcl_ok(); }

        }



        /// <summary>
        /// Implementation of IDisposable
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {

                // Free any other managed objects here.
            }
            // Free any unmanaged objects here.
            RCLReturnValues retVal = (RCLReturnValues)rcl_shutdown();
            switch (retVal)
            {
                case RCLReturnValues.RCL_RET_OK:
                    break;
                case RCLReturnValues.RCL_RET_NOT_INIT:
                    //throw new RCLNotInitException ();
                    break;
                case RCLReturnValues.RCL_RET_ERROR:
                    //throw new RCLErrorException (RCLErrorHandling.GetRMWErrorState());
                    break;
                default:
                    break;
            }
            disposed = true;
        }
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the <see cref="rclcs.RCL"/> is reclaimed
        /// by garbage collection.
        /// </summary>
        ~RCLWindows()
        {
            Dispose(false);
        }



        //Native methods

        [DllImport(@"rcl.dll")]
        //static extern int rcl_init(int argc, [In, Out] String[] argv, rcl_allocator_t allocator);
        static extern int rcl_init(int argc, [In, Out] String[] argv, ref rcl_init_options_t options, ref rcl_context_t context);
        //static extern int rcl_init(int argc, char** argv, rcl_init_options_t* options, rcl_context_t* context);

        [DllImport(@"rcl.dll")]
        static extern rcl_init_options_t rcl_get_zero_initialized_init_options();

        [DllImport(@"rcl.dll")]
        static extern rcl_context_t rcl_get_zero_initialized_context();

        [DllImport(@"rcl.dll")]
        static extern int rcl_init_options_init(ref rcl_init_options_t options, rcl_allocator_t allocator);

        [DllImport(@"rcl.dll")]
        static extern int rcl_shutdown();

        [DllImport(@"rcl.dll")]
        static extern UInt64 rcl_get_instance_id();

        [DllImport(@"rcl.dll")]
        static extern bool rcl_ok();

    }
}