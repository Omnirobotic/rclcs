using System;
using System.Runtime.InteropServices;

namespace rclcs
{
	internal class rcl_client_linux:rcl_client_base,IDisposable
	{
	    private IntPtr ResponsePtr;
	    private IntPtr RequestPtr;
		public rcl_client_linux(rcl_node_t _node, rosidl_service_type_support_t _typesupport,  string _service_name, rcl_client_options_t _options):base(_node,_typesupport,_service_name,_options)
		{
		    Console.WriteLine("Avant rcl_get_zero_initialized_client");
            native_handle = rcl_get_zero_initialized_client ();
		    Console.WriteLine("Avant rcl_client_init");
            rcl_client_init (ref native_handle, ref native_node, ref typesupport, service_name, ref options);
		    Console.WriteLine("Apres rcl_client_init");
		}
		~rcl_client_linux()
		{
			Dispose (false);
		}
		public static rcl_client_options_t get_default_options()
		{
			return rcl_client_get_default_options ();
		}

		public override T TakeResponse<T>(ref bool success)
		{
			success = false;
			rmw_request_id_t request_header = new rmw_request_id_t ();
			T response = new T ();
		    if (ResponsePtr != IntPtr.Zero)
		        Marshal.FreeHGlobal(ResponsePtr);

		    ResponsePtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
            response.GetData (ResponsePtr);
			int ret = rcl_take_response (ref native_handle, ref request_header, ResponsePtr);
			RCLReturnValues retVal = (RCLReturnValues)ret;
			switch (retVal) {
			case RCLReturnValues.RCL_RET_OK:
				success = true;
				response.SetData (ResponsePtr);
				break;
			case RCLReturnValues.RCL_RET_INVALID_ARGUMENT:
				throw new RCLInvalidArgumentException();

			case RCLReturnValues.RCL_RET_CLIENT_INVALID:
				throw new RCLClientInvalidException ();

			case RCLReturnValues.RCL_RET_ERROR:
				success = false;
				break;
			default:
				break;
			}
			return response;
		}
		public override void SendRequest<T>(T request)
		{
		    if (RequestPtr != IntPtr.Zero)
		        Marshal.FreeHGlobal(RequestPtr);

		    RequestPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
            request.GetData (RequestPtr);
			int ret = rcl_send_request (ref native_handle, RequestPtr, ref last_sequence_number);
			RCLReturnValues retVal = (RCLReturnValues)ret;
			switch (retVal) {
			case RCLReturnValues.RCL_RET_OK:
				break;
			case RCLReturnValues.RCL_RET_INVALID_ARGUMENT:
				throw new RCLInvalidArgumentException();

			case RCLReturnValues.RCL_RET_CLIENT_INVALID:
				throw new RCLClientInvalidException ();

			case RCLReturnValues.RCL_RET_ERROR:
				throw new RCLErrorException ();

			default:
				break;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposed)
				return; 

			if (disposing) {
				// Free any other managed objects here.
				//
			}
			rcl_client_fini(ref native_handle, ref native_node);
			// Free any unmanaged objects here.
			//
			disposed = true;
		}

		[DllImport(RCL.LibRCLPath)]
		static extern rcl_client_t rcl_get_zero_initialized_client();

		[DllImport(RCL.LibRCLPath)]
		static extern int rcl_client_init(ref rcl_client_t client, ref rcl_node_t node, ref rosidl_service_type_support_t type_support, [MarshalAs(UnmanagedType.LPStr)]string service_name, ref rcl_client_options_t options);

		[DllImport(RCL.LibRCLPath)]
		static extern int rcl_client_fini(ref rcl_client_t client, ref rcl_node_t node);

		[DllImport(RCL.LibRCLPath)]
		static extern rcl_client_options_t rcl_client_get_default_options();

		[DllImport(RCL.LibRCLPath)]
		static extern int rcl_send_request(ref rcl_client_t client, IntPtr ros_request, ref Int64 sequence_number);

		[DllImport(RCL.LibRCLPath)]
		static extern int rcl_take_response(ref rcl_client_t client, ref rmw_request_id_t request_header, IntPtr ros_response);

		[DllImport(RCL.LibRCLPath)]
		static extern string rcl_client_get_service_name(ref rcl_client_t client);

		[DllImport(RCL.LibRCLPath)]
		static extern IntPtr rcl_client_get_options(ref rcl_client_t client);

	}

}