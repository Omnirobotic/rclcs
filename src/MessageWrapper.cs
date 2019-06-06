using System;
using System.CodeDom;
using System.Runtime.InteropServices;
namespace rclcs
{
	
	public interface MessageWrapper:IDisposable
	{
	    void GetData(IntPtr _ptr);

	    void SetData(IntPtr _ptr);

	    void SyncDataOut();

	    void SyncDataIn();

	    //string GetStringValue();

	    void PrintValue();
	}

    /*public class Nested_t
    {
        public sbyte ANSWER;
        public Primitives primitives;

        public void Free()
        {

        }
    }

    public class Primitives
    {
        public Primitives Data;

        public Primitives(ref Primitives data)
        {
            Data = data;
        }

        public void SyncDataIn()
        {

        }

        public void SyncDataOut()
        {

        }
    }

    public struct Nested : MessageWrapper
    {

        private bool disposed;

        private Nested_t @__data;

        private Primitives @__primitives
;

        private Primitives @__two_primitives
;

        private Primitives @__up_to_three_primitives
;

        private Primitives @__unbounded_primitives
;

        public Nested(ref Nested_t _data) : this()
        {
            @__data = _data;
            disposed = false;
        }

        // Returns the underlying struct which is wrapped by this class. This struct will be sent in the end
        public Nested_t Data
        {
            get
            {
                return @__data;
            }
        }

        public sbyte ANSWER
        {
            get
            {
                return @__data.ANSWER;
            }
            set
            {
                @__data.ANSWER = value;
            }
        }

        public Primitives primitives
        {
            get
            {
                return @__primitives
;
            }
        }

        public Primitives two_primitives
        {
            get
            {
                return @__two_primitives
;
            }
        }

        public Primitives up_to_three_primitives
        {
            get
            {
                return @__up_to_three_primitives
;
            }
        }

        public Primitives unbounded_primitives
        {
            get
            {
                return @__unbounded_primitives
;
            }
        }

        public static System.Type GetMessageType()
        {
            return typeof(Nested_t);
        }

        public void GetData(out System.IntPtr _ptr)
        {
            _ptr = Marshal.AllocHGlobal(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, _ptr, false);
        }

        public void SetData(System.IntPtr _ptr)
        {
            var anotherP = (Nested)Marshal.PtrToStructure(_ptr, typeof(Nested));
            this.@__data = anotherP.@__data;
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }
            else
            {
                @__data.Free();
            }
            disposed = true;
        }

        // Needed to copy data from the struct in the class instance of the nested member the the real struct
        public void SyncDataOut()
        {
            @__data.primitives
 = @__primitives
.Data;
            @__primitives
.SyncDataOut();
        }

        // Needed to copy data from the real struct (that one that is stored in __data) to the struct in the class member of the nested type
        public void SyncDataIn()
        {
            @__primitives
 = new Primitives(ref __data.primitives
);
            @__primitives
.SyncDataIn();
        }
    }*/
}

