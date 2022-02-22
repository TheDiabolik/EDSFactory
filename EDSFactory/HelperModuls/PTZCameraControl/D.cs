using System;
using System.Collections;

namespace EDSFactory
{
	/// <summary>
	/// dot.NET Implementation of Pelco D Protocol
	/// </summary>
	public class D
	{

        private static D m_D;

        public static D GiveD()
        {
            if (m_D == null)
            {
                m_D = new D();

            }

            return m_D;
        }



		private static readonly byte STX = 0xFF;

		#region Pan and Tilt Commands
		#region Command1
		private const byte FocusNear =	0x01;
		private const byte IrisOpen =	0x02;
		private const byte IrisClose =	0x04;
		private const byte CameraOnOff = 0x08;
		private const byte AutoManualScan =	0x10;
		private const byte Sense =	0x80;
		#endregion

		#region Command2
		private const byte PanRight =	0x02;
		private const byte PanLeft =		0x04;
		private const byte TiltUp =		0x08;
		private const byte TiltDown =	0x10;
		private const byte ZoomTele =	0x20;
		private const byte ZoomWide =	0x40;
		private const byte FocusFar =	0x80;
		#endregion

		#region Data1
		private const byte PanSpeedMin = 0x00;
		private const byte PanSpeedMax = 0xFF;
		#endregion

		#region Data2
		private const byte TiltSpeedMin = 0x00;
		private const byte TiltSpeedMax = 0x3F;
		#endregion
		#endregion

		#region Enums
		public enum PresetAction {Set,Clear,Goto}
		public enum AuxAction {Set=0x09,Clear=0x0B}
		public enum Action {Start,Stop}
		public enum LensSpeed {Low=0x00,Medium=0x01,High=0x02,Turbo=0x03}
		public enum PatternAction {Start,Stop,Run}
		public enum SwitchAction {Auto=0x00,On=0x01,Off=0x02}
		public enum Switch {On=0x01,Off=0x02}
		public enum Focus {Near = FocusNear,Far = FocusFar}
		public enum Zoom {Wide = ZoomWide,Tele = ZoomTele}
		public enum Tilt {Up = TiltUp,Down = TiltDown}
		public enum Pan {Left = PanLeft,Right = PanRight}
		public enum Scan {Auto, Manual}
		public enum Iris {Open = IrisOpen,Close = IrisClose}
		#endregion

		#region Extended Command Set
		public byte[] Preset(uint deviceAddress, byte preset, PresetAction action)
		{
			byte m_action;
			switch (action)
			{
				case PresetAction.Set:
					m_action = 0x03;
					break;
				case PresetAction.Clear:
					m_action = 0x05;
					break;
				case PresetAction.Goto:
					m_action = 0x07;
					break;
				default:
					m_action = 0x03;
					break;
			}
            return Message.GetMessage(deviceAddress, 0x00, m_action, 0x00, preset);
		}

		public byte[] Flip(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x07,0x00,0x21);
		}

		public byte[] ZeroPanPosition(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x07,0x00,0x22);
		}

		public byte[] SetAuxiliary(uint deviceAddress,byte auxiliaryID, AuxAction action)
		{
			if(auxiliaryID<0x00)
				auxiliaryID = 0x00;
			else if(auxiliaryID>0x08)
				auxiliaryID = 0x08;
			return Message.GetMessage(deviceAddress,0x00,(byte)action,0x00,auxiliaryID);
		}

		public byte[] RemoteReset(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x0F,0x00,0x00);
		}
		public byte[] Zone(uint deviceAddress,byte zone, Action action)
		{
			if(zone<0x01 & zone>0x08)
				throw new Exception("Zone value should be between 0x01 and 0x08 include");
			byte m_action;
			if(action == Action.Start)
				m_action = 0x11;
			else
				m_action = 0x13;

			return Message.GetMessage(deviceAddress,0x00,m_action,0x00,zone);
		}

		public byte[] WriteToScreen(uint deviceAddress,string text)
		{
			if(text.Length > 40)
				text = text.Remove(40,text.Length-40);
			System.Text.Encoding encoding = System.Text.Encoding.ASCII;
			byte[] m_bytes = new byte[encoding.GetByteCount(text)*7];
			int i = 0;
			byte m_scrPosition;
			byte m_ASCIIchr;
			
			foreach(char ch in text)
			{
				m_scrPosition = Convert.ToByte(i/7);
				m_ASCIIchr = Convert.ToByte(ch);
				Array.Copy(Message.GetMessage(deviceAddress,0x00,0x15,m_scrPosition,m_ASCIIchr),0,m_bytes,i,7);
				i = i + 7;
			}

			return m_bytes;
		}

		public byte[] ClearScreen(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x17,0x00,0x00);
		}

		public byte[] AlarmAcknowledge(uint deviceAddress, uint alarmID)
		{
			if(alarmID < 1 & alarmID>8)
				throw new Exception("Only 8 alarms allowed for Pelco D implementation");
			return Message.GetMessage(deviceAddress,0x00,0x19,0x00,Convert.ToByte(alarmID));
		}

		public byte[] ZoneScan(uint deviceAddress,Action action)
		{
			byte m_action;
			if(action == Action.Start)
				m_action = 0x1B;
			else
				m_action = 0x1D;
			return Message.GetMessage(deviceAddress,0x00,m_action,0x00,0x00);
		}

		public byte[] Pattern(uint deviceAddress,PatternAction action)
		{
			byte m_action;
			switch (action)
			{
				case PatternAction.Start:
					m_action = 0x1F;
					break;
				case PatternAction.Stop:
					m_action = 0x21;
					break;
				case PatternAction.Run:
					m_action = 0x23;
					break;
				default:
					m_action = 0x23;
					break;
			}
			return Message.GetMessage(deviceAddress,0x00,m_action,0x00,0x00);
		}

		public byte[] SetZoomLensSpeed(uint deviceAddress, LensSpeed speed)
		{
			return Message.GetMessage(deviceAddress,0x00,0x25,0x00,(byte)speed);
		}

		public byte[] SetFocusLensSpeed(uint deviceAddress, LensSpeed speed)
		{
			return Message.GetMessage(deviceAddress,0x00,0x27,0x00,(byte)speed);
		}

		public byte[] ResetCamera(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x29,0x00,0x00);
		}
		public byte[] AutoFocus(uint deviceAddress, SwitchAction action)
		{
			return Message.GetMessage(deviceAddress,0x00,0x2B,0x00,(byte)action);
		}
		public byte[] AutoIris(uint deviceAddress, SwitchAction action)
		{
			return Message.GetMessage(deviceAddress,0x00,0x2D,0x00,(byte)action);
		}
		public byte[] AGC(uint deviceAddress, SwitchAction action)
		{
			return Message.GetMessage(deviceAddress,0x00,0x2F,0x00,(byte)action);
		}
		public byte[] BackLightCompensation(uint deviceAddress, Switch action)
		{
			return Message.GetMessage(deviceAddress,0x00,0x31,0x00,(byte)action);
		}
		public byte[] AutoWhiteBalance(uint deviceAddress, Switch action)
		{
			return Message.GetMessage(deviceAddress,0x00,0x33,0x00,(byte)action);
		}

		public byte[] EnableDevicePhaseDelayMode(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x35,0x00,0x00);
		}
		public byte[] SetShutterSpeed(uint deviceAddress,byte speed)
		{
			return Message.GetMessage(deviceAddress,0x00,0x37,speed,speed);//Not sure about
		}
		public byte[] AdjustLineLockPhaseDelay(uint deviceAddress)
		{			
			return Message.GetMessage(deviceAddress,0x00,0x39,0x00,0x00);
            throw new Exception("Did not implemented");
		}
		public byte[] AdjustWhiteBalanceRB(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x3B,0x00,0x00);
            throw new Exception("Did not implemented");
		}
		public byte[] AdjustWhiteBalanceMG(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x3D,0x00,0x00);
            throw new Exception("Did not implemented");
			
		}
		public byte[] AdjustGain(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x3F,0x00,0x00);
            throw new Exception("Did not implemented");
			
		}
		public byte[] AdjustAutoIrisLevel(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x41,0x00,0x00);
            throw new Exception("Did not implemented");
			
		}
		public byte[] AdjustAutoIrisPeakValue(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x43,0x00,0x00);
            throw new Exception("Did not implemented");
			
		}
		public byte[] Query(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x45,0x00,0x00);
            throw new Exception("Did not implemented");
			
		}
		#endregion

		#region Base Command Set

		public byte[] CameraSwitch(uint deviceAddress,Switch action)
		{
			byte m_action = CameraOnOff;
			if(action == Switch.On)
				m_action = CameraOnOff + Sense;
			return Message.GetMessage(deviceAddress,m_action,0x00,0x00,0x00);
			
		}

		public byte[] CameraIrisSwitch(uint deviceAddress,Iris action)
		{
			return Message.GetMessage(deviceAddress,(byte)action,0x00,0x00,0x00);
		}

		public byte[] CameraFocus(uint deviceAddress,Focus action)
		{
			if(action == Focus.Near)
				return Message.GetMessage(deviceAddress,(byte)action,0x00,0x00,0x00);
			else
				return Message.GetMessage(deviceAddress,0x00,(byte)action,0x00,0x00);
		}

		public byte[] CameraZoom(uint deviceAddress,Zoom action)
		{
			return Message.GetMessage(deviceAddress,0x00,(byte)action,0x00,0x00);
		}

		public byte[] CameraTilt(uint deviceAddress,Tilt action, uint speed)
		{
			if(speed<TiltSpeedMin)
				speed = TiltSpeedMin;
			if(speed>TiltSpeedMax)
				speed = TiltSpeedMax;

			return Message.GetMessage(deviceAddress,0x00,(byte)action,0x00,(byte)speed);
		}

		public byte[] CameraPan(uint deviceAddress,Pan action, uint speed)
		{
			if(speed<PanSpeedMin)
				speed = PanSpeedMin;
			if(speed>PanSpeedMax)
				speed = PanSpeedMax;

			return Message.GetMessage(deviceAddress,0x00,(byte)action,(byte)speed,0x00);
		}

		public byte[] CameraPanTilt(uint deviceAddress,Pan panAction, uint panSpeed, Tilt tiltAction, uint tiltSpeed)
		{
			byte[] m_bytes = new byte[8];
			byte[] m_tiltMessage = CameraTilt(deviceAddress,tiltAction,tiltSpeed);
			byte[] m_panMessage = CameraPan(deviceAddress,panAction,panSpeed);
			/*m_bytes[0] = m_tiltMessage[0];
			m_bytes[1] = m_tiltMessage[1];
			m_bytes[2] = m_tiltMessage[2];
			m_bytes[3] = (byte)(m_tiltMessage[3]+m_panMessage[3]);
			m_bytes[4] = (byte)(m_tiltMessage[4]+m_panMessage[4]);
			m_bytes[5] = (byte)(m_tiltMessage[5]+m_panMessage[5]);
			m_bytes[6] = m_tiltMessage[6];
			m_bytes[7] = m_tiltMessage[7];*/
			m_bytes = Message.GetMessage(deviceAddress,0x00,(byte)(m_tiltMessage[3]+m_panMessage[3]),
				m_panMessage[4],m_tiltMessage[5]);
			return m_bytes;

		}

		public byte[] CameraStop(uint deviceAddress)
		{
			return Message.GetMessage(deviceAddress,0x00,0x00,0x00,0x00);
		}

		public byte[] CameraScan(uint deviceAddress,Scan scan)
		{
			byte m_byte = AutoManualScan;
			if(scan == Scan.Auto)
				m_byte = AutoManualScan+Sense;

			return Message.GetMessage(deviceAddress,m_byte,0x00,0x00,0x00);

		}



		#endregion



		public struct Message
		{
			public static byte Address;
            public static byte CheckSum;
			public static byte Command1,Command2,Data1,Data2;

			public static byte[] GetMessage(uint address, byte command1, byte command2, byte data1, byte data2)
			{
				if (address<1 & address>256)
					throw new Exception("Protocol Pelco D support 256 devices only");
				
				Address = Byte.Parse((address).ToString());
				Data1 = data1;
				Data2 = data2;
				Command1 = command1;
				Command2 = command2;

               // CheckSum = (byte)(STX ^ Address ^ Command1 ^ Command2 ^ Data1 ^ Data2);
                //CheckSum = (byte)(Address ^ Command1 ^ Command2 ^ Data1 ^ Data2);
                CheckSum = (byte)(Address + Command1 + Command2 + Data1 + Data2);
                //CheckSum = (byte)(STX + Address + Command1 + Command2 + Data1 + Data2);
				return new byte[]{STX,Address,Command1,Command2,Data1,Data2,CheckSum};
			}
			
		}
	}
}
