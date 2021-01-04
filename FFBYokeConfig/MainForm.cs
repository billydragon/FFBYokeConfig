using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.InteropServices;


namespace FFBYokeConfig
{

    public partial class MainForm : Form
    {

        public List<string> GainsName = new List<string>
        {
            "Total Gains",
            "Constant Gain",
            "Ramp Gain",
            "Square Gain",
            "Sine Gain",
            "Triangle Gain",
            "SawToothDown Gain",
            "SawToothUp Gain",
            "Spring Gain",
            "Damper Gain",
            "Inertia Gain",
            "Friction Gain",
            "Custom Gain"
        };

        public List<string> DataTypeName = new List<string>
        {
            "Gains Memory",
            "Pids Memory",
            "System Memory",
            "Memory",
            "Gains Eeprom",
            "Pids Eeprom",
            "System Eeprom",
            "Eeprom",
            "Control Command",
            "Reset Default"
        };

        public List<string> CommandTypeName = new List<string>
        {
            "Read",
            "Write",
            "Load",
            "Save",
            "Control"
        };

        public List<string> AxisName = new List<string>
        {
            " X",
            " Y",
        };

        public enum DATA_TYPE: byte
        {
            Gains_Memory = 0x01,
            Pids_Memory,
            System_Memory,
            All_Memory,
            Gains_Eeprom,
            Pids_Eeprom,
            System_Eeprom,
            All_Eeprom,
            Control_CMD,
            Reset_Default
        };

        public enum SYS_CTRL_NAME
        {
             Motor_Inv_X = 0,
             Motor_Inv_Y,
             Swap_XYforces,
             Auto_Calibration,
             Motor_DIR_Delay,
             Reserv_1,
             Reserv_2,
             Reserv_3
        };

        public enum COMMAND_TYPE: byte
            {
                Read_Memory = 0x10,
                Write_Memory,
                Load_Eeprom,
                Save_Eeprom,
                Control
            };
        
       
        public List<string> PIDName = new List<string>
        {
            "MaxOutput",
            "SampleTime",
            "Kp",
            "Ki",
            "Kd",
        };

        public static ListBoxLog lbLog;
        private Thread thread_Logger;

        public Config  cfg = new Config();
        //public Gains[] m_gains = new Gains[2];

        // public static List<float[]> Pids = new List<float[]>();
        //public  UInt32[,] Pids = new UInt32[2,5];
        public float[,] Pids = new float[2, 5];
        // public static List<byte[]> Gains = new List<byte[]>();
        public  byte[,] Gains = new byte[2,13];
  
        public byte[] system_configs = new byte[8];

        NumericUpDown[,] numUDGainsArray = new NumericUpDown[2, 13];
        Label[,] lbGainsArray = new Label[2, 13];
        TextBox[,] tboxPIDArray = new TextBox[2, 5];
        Label[,] lbPIDArray = new Label[2, 5];

            /// <summary>
            /// Helper method to determin if invoke required, if so will rerun method on correct thread.
            /// if not do nothing.
            /// </summary>
            /// <param name="c">Control that might require invoking</param>
            /// <param name="a">action to preform on control thread if so.</param>
            /// <returns>true if invoke required</returns>
          
        
        public  static bool ControlInvokeRequired(Control c, Action a)
            {
                if (c.InvokeRequired) c.Invoke(new MethodInvoker(delegate
                {
                    a();
                }));
                else return false;

                return true;
            }

         

        public MainForm()
        {
            InitializeComponent();
            Initial_Control_Arrays();

            lbLog = new ListBoxLog(listBoxInfoTrace);
            thread_Logger = new Thread(LogStuffThread);
            thread_Logger.IsBackground = true;
            thread_Logger.Start();      

        }

        private void Initial_Control_Arrays()
        {
            for (int i = 0; i < numUDGainsArray.GetLength(0); i++)
            {
                for (int j = 0; j < numUDGainsArray.GetLength(1); j++)
                {
                    // instance the control
                    numUDGainsArray[i, j] = new NumericUpDown();
                    lbGainsArray[i, j] = new Label();
                    // set some initial properties
                    numUDGainsArray[i, j].Name = "numUD" + GainsName.ElementAt(j).Replace(" ", "_") + AxisName.ElementAt(i).Replace(" ", "_");
                    lbGainsArray[i, j].Name = "lb" + GainsName.ElementAt(j).Replace(" ", "_") + AxisName.ElementAt(i).Replace(" ", "_");
                    numUDGainsArray[i, j].TextAlign = HorizontalAlignment.Right;
                    numUDGainsArray[i, j].TabIndex = (20 + (i * 13) + j); //"";
                    lbGainsArray[i, j].Text = GainsName.ElementAt(j) + AxisName.ElementAt(i) + ":";
                    // add to form
                    this.Controls.Add(numUDGainsArray[i, j]);
                    this.Controls.Add(lbGainsArray[i, j]);
                    numUDGainsArray[i, j].Parent = gbGains;
                    lbGainsArray[i, j].Parent = gbGains;
                    // set position and size
                    numUDGainsArray[i, j].Size = new Size(50, 25);
                    numUDGainsArray[i, j].Location = new Point(150 + i * 190, 20 + j * 28);
                    lbGainsArray[i, j].Size = new Size(130, 25);
                    lbGainsArray[i, j].Location = new Point(20 + i * 190, 25 + j * 28);
                    numUDGainsArray[i, j].Leave += new System.EventHandler(this.GainsArrayLeave);
                    numUDGainsArray[i, j].KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GainsArrayKeyPress);

                }
            }

            for (int i = 0; i < tboxPIDArray.GetLength(0); i++)
            {
                for (int j = 0; j < tboxPIDArray.GetLength(1); j++)
                {
                    tboxPIDArray[i, j] = new TextBox();
                    lbPIDArray[i, j] = new Label();
                    tboxPIDArray[i, j].Name = "tbox" + PIDName.ElementAt(j).Replace(" ", "_") + AxisName.ElementAt(i).Replace(" ", "_");
                    lbPIDArray[i, j].Name = "lb" + PIDName.ElementAt(j).Replace(" ", "_") + AxisName.ElementAt(i).Replace(" ", "_");
                    lbPIDArray[i, j].Text = PIDName.ElementAt(j) + AxisName.ElementAt(i) + ":";
                    tboxPIDArray[i, j].TextAlign = HorizontalAlignment.Right;
                    tboxPIDArray[i, j].Text = "0.00";
                    this.Controls.Add(tboxPIDArray[i, j]);
                    this.Controls.Add(lbPIDArray[i, j]);
                    tboxPIDArray[i, j].Parent = gbPID;
                    lbPIDArray[i, j].Parent = gbPID;
                    tboxPIDArray[i, j].TabIndex = (60 + (i * 5) + j);
                    // set position and size
                    tboxPIDArray[i, j].Size = new Size(40, 25);
                    tboxPIDArray[i, j].Location = new Point(100 + i * 135, 20 + j * 30);
                    lbPIDArray[i, j].Size = new Size(80, 25);
                    lbPIDArray[i, j].Location = new Point(10 + i * 140, 22 + j * 30);
                    tboxPIDArray[i, j].Leave += new System.EventHandler(this.PIDArrayLeave);
                    tboxPIDArray[i, j].KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PIDArrayKeyPress);

                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            this.listBoxSerialPorts.Items.Clear();

            string[] portNames = SerialPort.GetPortNames();
            if (portNames.Length == 0)
            {
                this.listBoxSerialPorts.Items.Add("- None -");
                this.listBoxSerialPorts.ForeColor = Color.Red;
                Log(ListBoxLog.Level.Error, "ComPort Not Found.");
                btnOpenPort.Enabled = false;
            }
            else
            {
                this.listBoxSerialPorts.ForeColor = Color.Black;
                string[] array = portNames;
                for (int i = 0; i < array.Length; i++)
                {
                    string item = array[i];
                    this.listBoxSerialPorts.Items.Add(item);
                    this.listBoxSerialPorts.SelectedIndex = 0;
                    Log(ListBoxLog.Level.Info, "Found: " + item);
                }
                btnOpenPort.Enabled = true;
            }
            btnReadYoke.Enabled = false;
            buttonWriteYoke.Enabled = false;
            LoadConfig();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SaveConfig();
                
                if (serialPort1.IsOpen)
                {
                    string message = "Do you want to save to EEPROM before exit?";
                    string title = "Exit FFBYoke Configuration.";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show(message, title, buttons);
                    if (result == DialogResult.Yes)
                    {
                        Send_Save_Eeprom();
                    }
                    else
                    {
                    
                    }
                    Comport_disconnect();

                }
                lbLog.Dispose();
            }
            catch(Exception ex)
            {

            }

        }

        private void Comport_connect()
        {

            serialPort1.PortName = listBoxSerialPorts.Text;
            serialPort1.BaudRate = 115200;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.DataBits = 8;
            serialPort1.Handshake = Handshake.None;
            serialPort1.RtsEnable = true;

            try
            {
                serialPort1.Open();
                if (serialPort1.IsOpen)
                {
                    Log(ListBoxLog.Level.Debug, listBoxSerialPorts.Text + " Opened");
                    btnOpenPort.Text = "CLOSE";
                    buttonWriteYoke.Enabled = true;
                    btnReadYoke.Enabled = true;
                    btnSaveDefault.Enabled = true;
                    btnLoadEEP.Enabled = true;
                    btnSaveEEP.Enabled = true;
                    btnCALIBRATION.Enabled = true;

                }

            }
            catch (Exception ex)
            {

                Log(ListBoxLog.Level.Error, listBoxSerialPorts.Text + " open error: " + ex.Message);
            }


        }

        private void Comport_disconnect()
        {
            try
            {
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                Log(ListBoxLog.Level.Error, "Serial Port Closing Error: " + ex.Message);
            }
            btnOpenPort.Text = "OPEN";
            Log(ListBoxLog.Level.Debug, listBoxSerialPorts.Text + " Closed.");
            buttonWriteYoke.Enabled = false;
            btnReadYoke.Enabled = false;
            btnReadYoke.Enabled = false;
            btnSaveDefault.Enabled = false;
            btnLoadEEP.Enabled = false;
            btnSaveEEP.Enabled = false;
            btnCALIBRATION.Enabled = false;
        }


        private void PIDArrayKeyPress(object sender, KeyPressEventArgs e)
        {
            int idx = 0, pos = 0;

            TextBox txtbox = sender as TextBox;
            
            if (e.KeyChar != (char)Keys.Enter)
                return;

                if (txtbox.Name.Contains("_X"))
            {
                idx = 0;
            }
            else if(txtbox.Name.Contains("_Y"))
            {
                  idx = 1;

            }

                for (int j = 0; j < tboxPIDArray.GetLength(1); j++)
                {
                    if (tboxPIDArray[idx, j].Name == txtbox.Name)
                    {
                         pos = j;
                    if (txtbox.Text == "")
                        txtbox.Text = "0.00";
                    Pids.SetValue(float.Parse(tboxPIDArray[idx, j].Text), idx, j);
                }
                }
            if (!serialPort1.IsOpen)
                return;
            if (chkBoxAutoSave.Checked)
                Write_Pids_Memory(idx, pos, (float)Pids.GetValue(idx, pos));
                Log(ListBoxLog.Level.Warning, "Update " + PIDName.ElementAt(pos) + AxisName.ElementAt(idx) + ": " + Pids.GetValue(idx, pos).ToString());

        }

        private void GainsArrayKeyPress(object sender, KeyPressEventArgs e)
        {
            int idx = 0, pos = 0;
            
            NumericUpDown numbox = sender as NumericUpDown;

            if (e.KeyChar != (char)Keys.Enter)
                return;

            if (numbox.Name.Contains("_X"))
            {
                idx = 0;
            }
            else if (numbox.Name.Contains("_Y"))
            {
                idx = 1;

            }

            for (int j = 0; j < numUDGainsArray.GetLength(1); j++)
            {
                if (numUDGainsArray[idx, j].Name == numbox.Name)
                {
                    pos = j;
                    Gains.SetValue((byte)numUDGainsArray[idx, j].Value, idx, j);
                }
            }

            if (!serialPort1.IsOpen)
                return;
            if (chkBoxAutoSave.Checked)
            {
                Write_Gains_Memory(idx, pos, (byte)Gains.GetValue(idx, pos));
                Log(ListBoxLog.Level.Warning, "Update " + GainsName.ElementAt(pos) + AxisName.ElementAt(idx) + ": " + Gains.GetValue(idx, pos).ToString());

            }

        }

        private void GainsArrayLeave(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            NumericUpDown nb = sender as NumericUpDown;
            for (int i = 0; i < numUDGainsArray.GetLength(0); i++)
            {
                for (int j = 0; j < numUDGainsArray.GetLength(1); j++)
                {
                    if(numUDGainsArray[i,j].Name == nb.Name)
                    {
                      
                       Gains.SetValue((byte)numUDGainsArray[i, j].Value,i,j);

                        if (!serialPort1.IsOpen)
                            return;

                        if (chkBoxAutoSave.Checked)
                        {
                            Write_Gains_Memory(i, j, (byte)Gains.GetValue(i, j));
                           // Log(ListBoxLog.Level.Debug, "Auto Update " + GainsName.ElementAt(j) + AxisName.ElementAt(i) + ": " + Gains.GetValue(i, j).ToString());
                        }

                    }
                }
            }

            
        }

        private void PIDArrayLeave(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            TextBox tb = sender as TextBox;
           
            for (int i = 0; i < tboxPIDArray.GetLength(0); i++)
            {
                for (int j = 0; j < tboxPIDArray.GetLength(1); j++)
                {
                    if (tboxPIDArray[i, j].Name == tb.Name)
                    {
                        if (tb.Text == "")
                            tb.Text = "0.00";
                        Pids.SetValue(float.Parse(tboxPIDArray[i, j].Text),i,j);

                        if (!serialPort1.IsOpen)
                            return;
                        if (chkBoxAutoSave.Checked)
                            Write_Pids_Memory(i, j, (float)Pids.GetValue(i, j));

                       // float p = (float)Pids.GetValue(i, j);
                        //Log(ListBoxLog.Level.Debug, "Auto Update " + PIDName.ElementAt(j) + AxisName.ElementAt(i) + ": " + Pids.GetValue(i,j).ToString());
                        
                    }
                }
            }
        }
        private void Send_Save_Eeprom()
        {
            if (!serialPort1.IsOpen)
                return;
            
                    byte[] cmd = new byte[2]; 
                    cmd[0] = (byte)COMMAND_TYPE.Save_Eeprom;
                    cmd[1] = (byte)DATA_TYPE.All_Eeprom;
                    
                    serialPort1.Write(cmd, 0, cmd.Length);
                    Log(ListBoxLog.Level.Debug,"Save config to EEPROM.");
                    
         }

           

         private void Send_Load_Config()
        {
            byte[] cmd = new byte[2];
            cmd[0] = (byte)COMMAND_TYPE.Load_Eeprom;
            cmd[1] = (byte)DATA_TYPE.All_Eeprom;

            serialPort1.Write(cmd, 0, cmd.Length);

            Log(ListBoxLog.Level.Debug, "Load config from EEPROM.");
            
        }
        private void Command_LOAD_USER_EEPROM()
        {
            Send_Load_Config();

        }


        private void Write_Gains_Memory(int idx, int pos, byte _gain)
        {
            if (!serialPort1.IsOpen)
                return;
            byte[] cmd = new byte[6];

            cmd[0] = (byte)COMMAND_TYPE.Write_Memory;
            cmd[1] = (byte)DATA_TYPE.Gains_Memory;
            cmd[2] = (byte)idx;
            cmd[3] = (byte)pos;
            cmd[4] = 1;
            cmd[5] = _gain;
            serialPort1.Write(cmd, 0, cmd.Length);

            Log(ListBoxLog.Level.Send, "Write " + GainsName.ElementAt(pos) + AxisName.ElementAt(idx) + ": " + _gain.ToString() + " to Memory");
            //Thread.Sleep((int)numUD_DELAYS.Value);
        }

        private void Write_SYSCONTROL_Memory(int _index, byte _data)
        {
            if (!serialPort1.IsOpen)
                return;
           
            byte[] cmd = new byte[6];
            cmd[0] = (byte)COMMAND_TYPE.Write_Memory;
            cmd[1] = (byte)DATA_TYPE.System_Memory;
            cmd[2] = 0;
            cmd[3] = (byte)_index;
            cmd[4] = 1;
            cmd[5] = _data;                           //ConvertBoolArrayToByte(system_flags);

            serialPort1.Write(cmd, 0, cmd.Length);
            String s = "Write System Config Byte [";
            if (_index == 0)
                   s = "Write System Flags Byte  [";
            Log(ListBoxLog.Level.Send, s + _index.ToString() + "]: " + _data.ToString());

            //Thread.Sleep((int)numUD_DELAYS.Value);
        }


        private void Write_Pids_Memory(int idx, int pos, float _pid)
        {
            if (!serialPort1.IsOpen)
                return;
            byte[] cmd = new byte[5]; //Write Gains
            byte[] bOut = BitConverter.GetBytes(_pid);

            cmd[0] = (byte)COMMAND_TYPE.Write_Memory;
            cmd[1] = (byte)DATA_TYPE.Pids_Memory;
            cmd[2] = (byte)idx;
            cmd[3] = (byte)pos;
            cmd[4] = (byte)bOut.Length;

            serialPort1.Write(cmd, 0, cmd.Length);
            serialPort1.Write(bOut, 0, bOut.Length);
            
            Log(ListBoxLog.Level.Send, "Write " + PIDName.ElementAt(pos) + AxisName.ElementAt(idx) + ": " + _pid.ToString() + " to Memory");
            //Thread.Sleep((int)numUD_DELAYS.Value);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int head, idx, cmdlen, cmd, pos;
            String source;
            while (serialPort1.BytesToRead > 0)
            {
                Thread.Sleep((int)numUD_DELAYS.Value);
                head = serialPort1.ReadByte();
                    if ((head == (byte)COMMAND_TYPE.Read_Memory) || (head == (byte)COMMAND_TYPE.Load_Eeprom))    // Proccess Reading Memory
                    {
                        cmd = serialPort1.ReadByte();

                        switch (cmd)        //2
                        {
                            case (byte)DATA_TYPE.Gains_Memory:                  
                            case (byte)DATA_TYPE.Gains_Eeprom:
                                idx = serialPort1.ReadByte();               // 3 Axis index
                                pos = serialPort1.ReadByte();               // 4
                                cmdlen = serialPort1.ReadByte();            // 5 lenght of data
                                for (int j = 0; j < cmdlen; j++)
                                {
                                  byte data = (byte)serialPort1.ReadByte();
                                    Gains.SetValue(data, idx, pos + j);
                                    Log(ListBoxLog.Level.Receive, "Received " + DataTypeName.ElementAt(cmd - 1) + " : " + GainsName.ElementAt(pos + j) + AxisName.ElementAt(idx) + " = " + Gains.GetValue(idx, pos + j).ToString());
                                }
                                break;

                            case (byte)DATA_TYPE.Pids_Memory:
                                   
                            case (byte)DATA_TYPE.Pids_Eeprom:
                                    source = DataTypeName.ElementAt(cmd - 1);
                                    idx = serialPort1.ReadByte();               //3 Axis index
                                pos = serialPort1.ReadByte();               // 4
                                cmdlen = (serialPort1.ReadByte() / 4);      //5 lenght of data

                                for (int j = 0; j < cmdlen; j++)
                                {
                                    byte[] f = new byte[4];
                                    f[0] = (byte)serialPort1.ReadByte();
                                    f[1] = (byte)serialPort1.ReadByte();
                                    f[2] = (byte)serialPort1.ReadByte();
                                    f[3] = (byte) serialPort1.ReadByte();
                                    
                                    float val = System.BitConverter.ToSingle(f, 0);
                                    Pids.SetValue(val, idx, (pos + j));

                                    Log(ListBoxLog.Level.Receive, "Received " + source +  " : " + PIDName.ElementAt(pos + j) + AxisName.ElementAt(idx) + " = " + val.ToString());
                                }

                                break;
                            case (byte)DATA_TYPE.System_Memory:
                            case (byte)DATA_TYPE.System_Eeprom:
                                    idx = serialPort1.ReadByte();               //3 Axis index
                                    pos = serialPort1.ReadByte();               // 4
                                    cmdlen = serialPort1.ReadByte();      //5 lenght of data
                                   
                                    for (int j = 0; j < cmdlen; j++)
                                    {
                                        byte data = (byte)serialPort1.ReadByte();
                                        system_configs[pos + j] = data;
                                    String  s = "Received System Config Byte [";
                                    if (pos == 0)
                                            s = "Received System Flags Byte  [";

                                    Log(ListBoxLog.Level.Receive, s + pos.ToString() +"]: " + data.ToString());
                                    }
                                      
                            break;
                            default:
                                break;

                        }

                    }


            }
            RefreshFormInfo();
            
        }

        private void RefreshFormInfo()
        {
            for (int i = 0; i < Gains.GetLength(0); i++)
            {
                for (int j = 0; j < Gains.GetLength(1); j++)
                {
                    byte g = (byte)Gains.GetValue(i, j);
                    if (g > 100)
                    {
                        g = 100;
                        Gains.SetValue(g, i, j);
                    }
                    //Check if invoke requied if so return - as i will be recalled in correct thread
                    if (ControlInvokeRequired(numUDGainsArray[i, j], () => RefreshFormInfo())) return;
                    numUDGainsArray[i, j].Value = (byte)Gains.GetValue(i, j);
                }
                for(int p = 0; p< Pids.GetLength(1);p++)
                {
                    String text = Pids.GetValue(i,p).ToString();

                    //Check if invoke requied if so return - as i will be recalled in correct thread
                    if (ControlInvokeRequired(tboxPIDArray[i, p], () => RefreshFormInfo())) return;
                   
                    tboxPIDArray[i, p].Text = text;

                }
            }
            chkBoxMotorInv_X.Checked = Convert.ToBoolean(system_configs[(byte)SYS_CTRL_NAME.Motor_Inv_X]);
            chkBoxMotorInv_Y.Checked = Convert.ToBoolean(system_configs[(byte)SYS_CTRL_NAME.Motor_Inv_Y]);
            chkBoxSwapXYfoces.Checked = Convert.ToBoolean(system_configs[(byte)SYS_CTRL_NAME.Swap_XYforces]);
            chkBoxAutoCalibration.Checked = Convert.ToBoolean(system_configs[(byte)SYS_CTRL_NAME.Auto_Calibration]);
            numUDMotor_Dir_Delay.Value = system_configs[(byte)SYS_CTRL_NAME.Motor_DIR_Delay];
        }

        private void btn_READ_YOKE_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                return;
            byte[] cmd = new byte[2];
            cmd[0] = (byte)COMMAND_TYPE.Read_Memory;
            cmd[1] = (byte)DATA_TYPE.All_Memory;

            serialPort1.Write(cmd, 0, cmd.Length);

            Log(ListBoxLog.Level.Debug, "Read config from Memory.");

        }

        private void btn_RESET_DEFAULT_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                return;
            byte[] cmd = new byte[2];
            cmd[0] = (byte)COMMAND_TYPE.Control;
            cmd[1] = (byte)DATA_TYPE.Reset_Default;
            
            serialPort1.Write(cmd, 0, cmd.Length);

            Log(ListBoxLog.Level.Debug, "Reset Default Configs.");

        }

        private void btn_OPEN_COMPORT_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                Comport_disconnect();
            }
            else
            {
                Comport_connect();
                Command_LOAD_USER_EEPROM();
            }
        }
        
        private void btn_WRITE_YOKE_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                return;
            Log(ListBoxLog.Level.Warning, "Writing Configs to Memory...");
            for (int idx = 0; idx < Gains.GetLength(0); idx++)
            {
                for (int j = 0; j < Gains.GetLength(1); j++)
                {
                    Write_Gains_Memory(idx, j, (byte)Gains.GetValue(idx, j));

                }

            }

            for (int idx = 0; idx < Pids.GetLength(0); idx++)  //Write Pids 
            {

                for (int j = 0; j < Pids.GetLength(1); j++)
                {

                    Write_Pids_Memory(idx, j, (float)Pids.GetValue(idx, j));

                }

            }
            
            for (int j = 0; j < system_configs.Length; j++)
            {
                Write_SYSCONTROL_Memory(j,system_configs[j]);
            }
            Log(ListBoxLog.Level.Warning, "Write all config to Memory Done.");

        }

        private void btn_SAVE_EEPROM_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                return;
            Send_Save_Eeprom();

        }

        private void btn_LOAD_EEPROM_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
                return;
            Command_LOAD_USER_EEPROM();
        }


        private void LogStuffThread()
        {
            // int number = 0;
            while (true)
            {
                //Log(Level.Info, "A info level message from thread # {0,0000}", number++);
                Thread.Sleep(1000);
            }
        }

        private void Log(ListBoxLog.Level level, string msg)
        {

            lbLog.Log(level, msg);

        }

        private string ByteToHexBitFiddle(byte[] bytes)
        {
            string hex = BitConverter.ToString(bytes);
            hex = hex.Replace("-", " ");
            return hex;
        }

        private void LoadConfig()
        {
            //string appPath = Path.GetDirectoryName(Application.ExecutablePath);

            if (!File.Exists("FFBYokeConfig.xml"))
            {
                Log(ListBoxLog.Level.Error, "FFBYokeConfig.xml file not found!.");
                return;
            }

            cfg = DeserializeFromXML();
            initControls();
        }

        private void initControls()
        {
            if (listBoxSerialPorts.Items.Contains(cfg.FFB_Port_Name)) 
                listBoxSerialPorts.Text = cfg.FFB_Port_Name;
            else if 
                (listBoxSerialPorts.Items.Count > 0) 
                listBoxSerialPorts.SelectedIndex = listBoxSerialPorts.Items.Count - 1;
            else
            {
                this.Log(ListBoxLog.Level.Error, "ComPorts not found.");

            }
            chkBoxAutoSave.Checked = cfg.FFB_Auto_Save;
            numUD_DELAYS.Value = cfg.FFB_Port_Delays;


        }

        private void SaveConfig()
        {
            if (cfg == null) cfg = new Config();
            try
            {

                cfg.FFB_Port_Name = listBoxSerialPorts.Text;
                cfg.FFB_Auto_Save = chkBoxAutoSave.Checked;
                cfg.FFB_Port_Delays = (int)numUD_DELAYS.Value;
                //Call Serialize
                SerializeToXML(cfg);

            }
            catch
            {
                Log(ListBoxLog.Level.Error, "Error: Write FFBYokeConfig.xml file.");

            }
        }

        public void SerializeToXML(Config cf)
        {
            //string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                TextWriter textWriter = new StreamWriter("FFBYokeConfig.xml");
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(textWriter, cf, ns);
                textWriter.Close();
            }
            catch { }
        }

        public Config DeserializeFromXML()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Config));
            try
            {
                TextReader textReader = new StreamReader("FFBYokeConfig.xml");
                Config cf = (Config)deserializer.Deserialize(textReader);
                textReader.Close();

                return cf;
            }
            catch { return null; }
        }
        private static byte ConvertBoolArrayToByte(bool[] source)
        {
            byte result = 0;
            // This assumes the array never contains more than 8 elements!
            int index = 8 - source.Length;

            // Loop through the array
            foreach (bool b in source)
            {
                // if the element is 'true' set the bit at that position
                if (b)
                    result |= (byte)(1 << (7 - index));

                index++;
            }

            return result;
        }

        private static bool[] ConvertByteToBoolArray(byte b)
        {
            // prepare the return result
            bool[] result = new bool[8];

            // check each bit in the byte. if 1 set to true, if 0 set to false
            for (int i = 0; i < 8; i++)
                result[i] = (b & (1 << i)) == 0 ? false : true;

            // reverse the array
            Array.Reverse(result);

            return result;
        }

        private void chkBoxMotorInv_X_CheckedChanged(object sender, EventArgs e)
        {
            int index = (int)SYS_CTRL_NAME.Motor_Inv_X;
            system_configs[index] = Convert.ToByte(chkBoxMotorInv_X.Checked);
            Log(ListBoxLog.Level.Warning, "Set Motor Invert X: " + system_configs[index].ToString());
            if (chkBoxAutoSave.Checked)
                Write_SYSCONTROL_Memory(index, system_configs[index]);
        }

        private void chkBoxMotorInv_Y_CheckedChanged(object sender, EventArgs e)
        {
            int index = (int)SYS_CTRL_NAME.Motor_Inv_Y;
            system_configs[index] = Convert.ToByte(chkBoxMotorInv_Y.Checked);
            Log(ListBoxLog.Level.Warning, "Set Motor Invert Y: " + system_configs[index].ToString());
            if (chkBoxAutoSave.Checked)
                Write_SYSCONTROL_Memory(index, system_configs[index]);
        }

        private void chkBoxSwapAxis_CheckedChanged(object sender, EventArgs e)
        {
            int index = (int)SYS_CTRL_NAME.Swap_XYforces;
            system_configs[index] = Convert.ToByte(chkBoxSwapXYfoces.Checked);
            Log(ListBoxLog.Level.Warning, "Set Swap XY Forces: " + system_configs[index].ToString());
            if (chkBoxAutoSave.Checked)
                Write_SYSCONTROL_Memory(index, system_configs[index]);
        }

        private void chkBoxAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            Log(ListBoxLog.Level.Warning, "Automatic update change to Memory: " + chkBoxAutoSave.Checked.ToString());
        }

        private void chkBoxAutoCalibration_CheckedChanged(object sender, EventArgs e)
        {
            int index = (int)SYS_CTRL_NAME.Auto_Calibration;
            system_configs[index] = Convert.ToByte(chkBoxAutoCalibration.Checked);
            Log(ListBoxLog.Level.Warning, "Set Auto Calibration: " + system_configs[index].ToString());
            if (chkBoxAutoSave.Checked)
                Write_SYSCONTROL_Memory(index, system_configs[index]);
        }


        private void numUDMotor_Dir_Delay_ValueChanged(object sender, EventArgs e)
        {
            int index = (int)SYS_CTRL_NAME.Motor_DIR_Delay;
            system_configs[index] = (byte)numUDMotor_Dir_Delay.Value;
            Log(ListBoxLog.Level.Warning, "Set Motor DIR Delays : " + system_configs[index].ToString());
            if (chkBoxAutoSave.Checked)
                Write_SYSCONTROL_Memory(index, system_configs[index]);

        }
    }
}
