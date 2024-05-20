/*
    This file is part of libEDSsharp.

    libEDSsharp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    libEDSsharp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with libEDSsharp.  If not, see <http://www.gnu.org/licenses/>.

    Copyright(c) 2019 Robin Cornelius <robin.cornelius@gmail.com>
    Copyright(c) 2024 Janez Paternoster
*/

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace libEDSsharp
{
    /// <summary>
    /// Object dictionary basic data types from CiA 301
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<DataType>))]
    public enum DataType
    {
        UNKNOWN = 0x00,
        BOOLEAN = 0x01,
        INTEGER8 = 0x02,
        INTEGER16 = 0x03,
        INTEGER32 = 0x04,
        UNSIGNED8 = 0x05,
        UNSIGNED16 = 0x06,
        UNSIGNED32 = 0x07,
        REAL32 = 0x08,
        VISIBLE_STRING = 0x09,
        OCTET_STRING = 0x0A,
        UNICODE_STRING = 0x0B,
        TIME_OF_DAY = 0x0C,
        TIME_DIFFERENCE = 0x0D,
        DOMAIN = 0x0F,
        INTEGER24 = 0x10,
        REAL64 = 0x11,
        INTEGER40 = 0x12,
        INTEGER48 = 0x13,
        INTEGER56 = 0x14,
        INTEGER64 = 0x15,
        UNSIGNED24 = 0x16,
        UNSIGNED40 = 0x18,
        UNSIGNED48 = 0x19,
        UNSIGNED56 = 0x1A,
        UNSIGNED64 = 0x1B
    }

    /// <summary>
    /// Object Dictionary object codes from CiA 301
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<ObjectType>))]
    public enum ObjectType
    {
        UNKNOWN = -1,
        /// <summary>
        /// An object with no data fields
        /// </summary>
        NULL = 0,
        /// <summary>
        /// Large variable amount of data e.g. executable program code
        /// </summary>
        DOMAIN = 2,
        /// <summary>
        /// Denotes a type definition such as a BOOLEAN, UNSIGNED16, FLOAT and so on
        /// </summary>
        DEFTYPE = 5,
        /// <summary>
        /// Defines a new record type e.g. the PDO mapping structure at 21h
        /// </summary>
        DEFSTRUCT = 6,
        /// <summary>
        /// A single value such as an UNSIGNED8, BOOLEAN, FLOAT, INTEGER16, VISIBLE STRING etc.
        /// </summary>
        VAR = 7,
        /// <summary>
        /// A multiple data field object where each data field is a
        /// simple variable of the SAME basic data type e.g. array of UNSIGNED16 etc.
        /// Sub-index 0 is of UNSIGNED8 and therefore not part of the ARRAY data
        /// </summary>
        ARRAY = 8,
        /// <summary>
        /// A multiple data field object where the data fields may be any combination of
        /// simple variables. Sub-index 0 is of UNSIGNED8 and sub-index 255 is of UNSIGNED32 and
        /// therefore not part of the RECORD data
        /// </summary>
        RECORD = 9,
    }

    /// <summary>
    /// Defines how the object can be changed from SDO
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<AccessSDO>))]
    public enum AccessSDO
    {
        /// <summary>
        /// no access
        /// </summary>
        no,

        /// <summary>
        /// read only access
        /// </summary>
        ro,

        /// <summary>
        /// write only access
        /// </summary>
        wo,

        /// <summary>
        /// read and write access
        /// </summary>
        rw
    }

    /// <summary>
    /// Defines how the object can be changed from PDO
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<AccessPDO>))]
    public enum AccessPDO
    {
        /// <summary>
        /// no access
        /// </summary>
        no,

        /// <summary>
        /// TPDO access
        /// </summary>
        t,

        /// <summary>
        /// RPDO access
        /// </summary>
        r,

        /// <summary>
        /// TPDO and RPDO access
        /// </summary>
        tr
    }

    /// <summary>
    /// Defines how the object can be changed from SRDO
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<AccessSRDO>))]
    public enum AccessSRDO
    {
        /// <summary>
        /// no access
        /// </summary>
        no = 0,

        /// <summary>
        /// SRDO TX access
        /// </summary>
        tx = 1,

        /// <summary>
        /// SRDO RX access
        /// </summary>
        rx = 2,

        /// <summary>
        /// SRDO TX or RX access
        /// </summary>
        trx = 3
    }

    /// <summary>
    /// CANopen [FileInfo] section, based on CiA306
    /// </summary>
    public class CanOpen_FileInfo
    {
        /// <summary>
        /// Actual file version (as string)
        /// </summary>
        public string FileVersion { get; set; }

        /// <summary>
        /// File description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// File creation time
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Name or a description of the file creator
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Time of last modification
        /// </summary>
        public DateTime ModificationTime { get; set; }

        /// <summary>
        /// Name or a description of the creator
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// New default object
        /// </summary>
        public CanOpen_FileInfo()
        {
            // make empty strings instead of null
            FileVersion = "";
            Description = "";
            CreatedBy = "";
            ModifiedBy = "";
        }
    }

    /// <summary>
    /// CANopen [DeviceInfo] section, based on CiA306
    /// </summary>
    public class CanOpen_DeviceInfo
    {
        /// <summary>
        /// Vendor name
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// Unique vendor-ID (index 1018, sub-index 01)
        /// </summary>
        public UInt32 VendorNumber { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Product code (index 1018, sub-index 02)
        /// </summary>
        public UInt32 ProductNumber { get; set; }

        /// <summary>
        /// Support of the baud rate 10 kbit/s
        /// </summary>
        public bool BaudRate_10 { get; set; }

        /// <summary>
        /// Support of the baud rate 20 kbit/s
        /// </summary>
        public bool BaudRate_20 { get; set; }

        /// <summary>
        /// Support of the baud rate 50 kbit/s
        /// </summary>
        public bool BaudRate_50 { get; set; }

        /// <summary>
        /// Support of the baud rate 125 kbit/s
        /// </summary>
        public bool BaudRate_125 { get; set; }

        /// <summary>
        /// Support of the baud rate 250 kbit/s
        /// </summary>
        public bool BaudRate_250 { get; set; }

        /// <summary>
        /// Support of the baud rate 500 kbit/s
        /// </summary>
        public bool BaudRate_500 { get; set; }

        /// <summary>
        /// Support of the baud rate 800 kbit/s
        /// </summary>
        public bool BaudRate_800 { get; set; }

        /// <summary>
        /// Support of the baud rate 1000 kbit/s
        /// </summary>
        public bool BaudRate_1000 { get; set; }

        /// <summary>
        /// Minimum size of a mappable object in bits, allowed for the PDO mapping on this CANopen device, usually 8
        /// </summary>
        public uint Granularity { get; set; }

        /// <summary>
        /// Number of the supported receive PDOs
        /// </summary>
        public UInt16 NrOfRxPDO { get; set; }

        /// <summary>
        /// Number of the supported transmit PDOs
        /// </summary>
        public UInt16 NrOfTxPDO { get; set; }

        /// <summary>
        /// Support of the LSS slave functionality, see CiA305
        /// </summary>
        public bool LssSlave { get; set; }

        /// <summary>
        /// Support of the LSS master functionality, see CiA305
        /// </summary>
        public bool LssMaster { get; set; }

        /// <summary>
        /// Support of the NodeGuarding slave
        /// </summary>
        public bool NodeGuardingSlave { get; set; }

        /// <summary>
        /// Support of the NodeGuarding master
        /// </summary>
        public bool NodeGuardingMaster { get; set; }

        /// <summary>
        /// Support of the NodeGuarding master
        /// </summary>
        public UInt16 NrOfNodeGuardingMonitoredNodes { get; set; }

        /// <summary>
        /// New default object
        /// </summary>
        public CanOpen_DeviceInfo()
        {
            // make empty strings instead of null
            VendorName = "";
            ProductName = "";
            BaudRate_10 = true;
            BaudRate_20 = true;
            BaudRate_50 = true;
            BaudRate_125 = true;
            BaudRate_250 = true;
            BaudRate_500 = true;
            BaudRate_1000 = true;
            Granularity = 8;
            NrOfRxPDO = 4;
            NrOfTxPDO = 4;
            LssSlave = true;
        }
    }

    /// <summary>
    /// CANopen [DeviceComissioning] section, based on CiA306, used for the device configuration file (DCF)
    /// </summary>
    public class CanOpen_DeviceComissioning
    {
        /// <summary>
        /// CANopen device’s address
        /// </summary>
        public uint NodeID { get; set; }

        /// <summary>
        /// Node name
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// CANopen device’s baudrate
        /// </summary>
        public UInt16 Baudrate { get; set; }

        /// <summary>
        /// Number of the network
        /// </summary>
        public UInt32 NetNumber { get; set; }

        /// <summary>
        /// Name of the network
        /// </summary>
        public string NetworkName { get; set; }

        /// <summary>
        /// CANopen device is the CANopen manager
        /// </summary>
        public bool CANopenManager { get; set; }

        /// <summary>
        /// Serial number (index 1018, sub-index 03)
        /// </summary>
        public UInt32 LSS_SerialNumber { get; set; }

        /// <summary>
        /// New default object
        /// </summary>
        public CanOpen_DeviceComissioning()
        {
            // make empty strings instead of null
            NodeName = "";
            NetworkName = "";
        }
    }

    /// <summary>
    /// Object Dictionary SubEntry on specific Subindex. Sorted dictionary of them
    /// is part of OdEntry. If OdEntry ObjectType is "record", then each SubEntry in the
    /// dictionary may be unique. If OdEntry ObjectType is "array", then some properties
    /// of all SubEntries must be equal. If OdEntry ObjectType is "var", then
    /// one SubEntry exists.
    /// </summary>
    public class OdSubEntry
    {
        /// <summary>
        /// Name of the sub entry. If OdEntry is "VAR", this property is not relevant.
        /// If null, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string SubParameterName { get; set; }

        /// <summary>
        /// Additonal parameter name, for the device configuration file (DCF).
        /// If null, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Denotation { get; set; }

        /// <summary>
        /// CANopen data type
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        /// CANopen SDO access permissions
        /// </summary>
        public AccessSDO AccessSDO { get; set; }

        /// <summary>
        /// CANopen PDO access permissions
        /// </summary>
        public AccessPDO AccessPDO { get; set; }

        /// <summary>
        /// CANopen SRDO access permissions.
        /// If 0 or "no", parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public AccessSRDO AccessSRDO { get; set; }

        /// <summary>
        /// Default value of the sub object.
        /// If null, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string DefaultValue { get; set; }

        /// <summary>
        /// Actual value, for the device configuration file (DCF).
        /// If null, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string ParameterValue { get; set; }

        /// <summary>
        /// Low limit for the value.
        /// If null, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string LowLimit { get; set; }

        /// <summary>
        /// High limit for the value.
        /// If null, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string HighLimit { get; set; }

        /// <summary>
        /// CanOpenNode OD exporter V4: Minimum length of a string that can be stored.
        /// If 0, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public UInt32 StringLengthMin { get; set; }

        /// <summary>
        /// New default object
        /// </summary>
        public OdSubEntry()
        {
        }

        /// <summary>
        /// Deep clone
        /// </summary>
        /// <returns>a deep clone</returns>
        public OdSubEntry Clone()
        {
            OdSubEntry newSubEntry = new OdSubEntry
            {
                SubParameterName = SubParameterName,
                Denotation = Denotation,
                DataType = DataType,
                AccessSDO = AccessSDO,
                AccessPDO = AccessPDO,
                AccessSRDO = AccessSRDO,
                DefaultValue = DefaultValue,
                ParameterValue = ParameterValue,
                LowLimit = LowLimit,
                HighLimit = HighLimit,
                StringLengthMin = StringLengthMin
            };
            return newSubEntry;
        }
    }

    /// <summary>
    /// Object Dictionary Entry on specific Index. Sorted dictionary of them
    /// is part of CanOpenDevice - CANopen Object Dictionary.
    /// </summary>
    public class OdEntry
    {
        /// <summary>
        /// If true, object is completelly skipped by CANopenNode exporters, etc.
        /// If false, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Disabled { get; set; }

        /// <summary>
        /// Name of the entry
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Additonal parameter name, for the device configuration file (DCF).
        /// If null, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Denotation { get; set; }

        /// <summary>
        /// Description of the Entry.
        /// If null, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Description { get; set; }

        /// <summary>
        /// CANopen Object Type
        /// </summary>
        public ObjectType ObjectType { get; set; }

        /// <summary>
        /// CANopen Complex Data Type, if ObjectType==RECORD. This property
        /// is informative and required for some exportrs. Complex data types
        /// are defined by OdSubEntries for each ODEntry individually.
        /// If 0, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public UInt16 ComplexDataType { get; set; }

        /// <summary>
        /// CanOpenNode OD exporter V4: it will generate a macro for each different CO_countLabel.
        /// For example, if four OD objects have "CO_countLabel" set to "TPDO", then
        /// macro "#define ODxyz_CNT_TPDO 4" will be generated by the OD exporter.
        /// If null, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string CountLabel { get; set; }

        /// <summary>
        /// CanOpenNode OD exporter V4: storage group into which the C variable will belong.
        /// If null, it will default to "RAM" and is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string StorageGroup { get; set; }

        /// <summary>
        /// CanOpenNode OD exporter V1.3: Flags for the PDO.
        /// If false, parameter is ignored by the JSON exporter.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool FlagsPDO { get; set; }

        /// <summary>
        /// Sorted dictionary of sub entries
        /// </summary>
        public SortedDictionary<string, OdSubEntry> SubObjects { get; set; }

        /// <summary>
        /// New empty object
        /// </summary>
        public OdEntry()
        {
            // make empty strings instead of null
            ParameterName = "";
            SubObjects = new SortedDictionary<string, OdSubEntry>();
        }

        /// <summary>
        /// Deep clone
        /// </summary>
        /// <returns>a deep clone</returns>
        public OdEntry Clone()
        {
            OdEntry newEntry = new OdEntry
            {
                Disabled = Disabled,
                ParameterName = ParameterName,
                Denotation = Denotation,
                Description = Description,
                ObjectType = ObjectType,
                ComplexDataType = ComplexDataType,
                CountLabel = CountLabel,
                StorageGroup = StorageGroup,
                FlagsPDO = FlagsPDO,
                SubObjects = new SortedDictionary<string, OdSubEntry>()
            };

            foreach (KeyValuePair<string, OdSubEntry> kvp in SubObjects)
                newEntry.SubObjects.Add(kvp.Key, kvp.Value.Clone());

            return newEntry;
        }
    }

    /// <summary>
    /// CANopen Device description object
    /// </summary>
    public class CanOpenDevice
    {
        /// <summary>
        /// CANopen [FileInfo] section, based on CiA306
        /// </summary>
        public CanOpen_FileInfo FileInfo { get; set; }

        /// <summary>
        /// CANopen [DeviceInfo] section, based on CiA306
        /// </summary>
        public CanOpen_DeviceInfo DeviceInfo { get; set; }

        /// <summary>
        /// CANopen [DeviceComissioning] section, based on CiA306, used for the device configuration file (DCF)
        /// </summary>
        public CanOpen_DeviceComissioning DeviceComissioning { get; set; }

        /// <summary>
        /// CANopen Object Dictionary
        /// </summary>
        public SortedDictionary<string, OdEntry> Objects { get; set; }

        /// <summary>
        /// New empty object
        /// </summary>
        public CanOpenDevice()
        {
            FileInfo = new CanOpen_FileInfo();
            DeviceInfo = new CanOpen_DeviceInfo();
            DeviceComissioning = new CanOpen_DeviceComissioning();
            Objects = new SortedDictionary<string, OdEntry>();
        }

        /// <summary>
        /// Create CanOpenDevice object from JSON string
        /// </summary>
        /// <param name="json">JSON string</param>
        /// <returns></returns>
        public CanOpenDevice(string json)
        {
            CanOpenDevice dev = JsonSerializer.Deserialize<CanOpenDevice>(json);

            FileInfo = dev.FileInfo;
            DeviceInfo = dev.DeviceInfo;
            DeviceComissioning = dev.DeviceComissioning;
            Objects = dev.Objects;
        }

        /// <summary>
        /// Export this class to the JSON string
        /// </summary>
        /// <returns>JSON string</returns>
        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
}
