﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Org.BouncyCastle.Security;

using PeterO.Cbor;

#pragma warning disable CS0618 // XXX is obsolete

namespace Com.AugustCellars.COSE
{

    public enum Tags
    { 
        [Obsolete]
        Encrypted = 16,
        [Obsolete]
        Enveloped =96,
        [Obsolete("Use Tags.Sign")]
        Signed = 98, 
        Sign = 98,
        MAC = 97, MAC0=17,
        [Obsolete("Use Sign1")]
        Signed0 =18,
        Sign1 = 18,
        Unknown = 0,
        Encrypt0 = 16,
        Encrypt = 96
    }

    public class RecordKeys
    {
        public static readonly CBORObject MsgType = CBORObject.FromObject(1);
        public static readonly CBORObject Protected = CBORObject.FromObject(2);
        public static readonly CBORObject Unprotected = CBORObject.FromObject(3);
        public static readonly CBORObject Payload = CBORObject.FromObject(4);
        public static readonly CBORObject Signatures = CBORObject.FromObject(5);
        public static readonly CBORObject Signature = CBORObject.FromObject(6);
        public static readonly CBORObject CipherText = CBORObject.FromObject(4);
        public static readonly CBORObject Recipients = CBORObject.FromObject(9);
        public static readonly CBORObject Tag = CBORObject.FromObject(10);
    };

    public class HeaderKeys
    {
        public static readonly CBORObject Algorithm = CBORObject.FromObject(1);
        public static readonly CBORObject Critical = CBORObject.FromObject(2);
        public static readonly CBORObject ContentType = CBORObject.FromObject(3);
        public static readonly CBORObject EphemeralKey = CBORObject.FromObject(-1);
        public static readonly CBORObject ECDH_SPK = CBORObject.FromObject(-2);
        public static readonly CBORObject StaticKey = CBORObject.FromObject(-2);
        public static readonly CBORObject ECDH_SKID = CBORObject.FromObject(-3);
        public static readonly CBORObject StaticKey_ID = CBORObject.FromObject(-3);
        public static readonly CBORObject KeyId = CBORObject.FromObject(4);
        public static readonly CBORObject IV = CBORObject.FromObject(5);
        public static readonly CBORObject PartialIV = CBORObject.FromObject(6);
        public static readonly CBORObject CounterSignature = CBORObject.FromObject(7);
        public static readonly CBORObject OperationTime = CBORObject.FromObject(8);
    }

    public enum AlgorithmValuesInt
    { 
        AES_GCM_128=1, AES_GCM_192=2, AES_GCM_256=3,
        HMAC_SHA_256_64=4, HMAC_SHA_256=5, HMAC_SHA_384=6, HMAC_SHA_512=7,

        AES_CBC_MAC_128_64 = 14, AES_CBC_MAC_128_128=25, AES_CBC_MAC_256_64 =15, AES_CBC_MAC_256_128=26,

        ChaCha20_Poly1305=24,

        AES_CCM_16_64_128 =10, AES_CCM_16_64_256=11, AES_CCM_64_64_128=12, AES_CCM_64_64_256=13,
        AES_CCM_16_128_128=30, AES_CCM_16_128_256=31, AES_CCM_64_128_128=32, AES_CCM_64_128_256=33,

        RSA_OAEP = -125, RSA_OAEP_256 = -126,

        AES_KW_128 = -3, AES_KW_192=-4, AES_KW_256=-5,
        DIRECT = -6,
        Direct_HKDF_HMAC_SHA_256=-10, Direct_HKDF_HMAC_SHA_512=-11,
        Direct_HKDF_AES_128=-12, Direct_HKDF_AES_256=-13,

        ECDSA_256 = -7, ECDSA_384=-35, ECDSA_512=-36,
        RSA_PSS_256 = -26, RSA_PSS_384=-27, RSA_PSS_512 = -28,
        EdDSA = -8,
        ECDH_ES_HKDF_256=-25, ECDH_ES_HKDF_512=-26,
        ECDH_SS_HKDF_256=-27, ECDH_SS_HKDF_512=-28,

        ECDH_ES_HKDF_256_AES_KW_128 = -29, ECDH_ES_HKDF_256_AES_KW_192 = -30, ECDH_ES_HKDF_256_AES_KW_256 = -31,
        ECDH_SS_HKDF_256_AES_KW_128 = -32, ECDH_SS_HKDF_256_AES_KW_192 = -33, ECDH_SS_HKDF_256_AES_KW_256 = -34,
    }

    public class AlgorithmValues
    {
        public static readonly CBORObject HKDF_HMAC_SHA_256 = CBORObject.FromObject(AlgorithmValuesInt.Direct_HKDF_HMAC_SHA_256);
        public static readonly CBORObject HKDF_HMAC_SHA_512 = CBORObject.FromObject(AlgorithmValuesInt.Direct_HKDF_HMAC_SHA_512);
        public static readonly CBORObject HKDF_AES_128 = CBORObject.FromObject(AlgorithmValuesInt.Direct_HKDF_AES_128);
        public static readonly CBORObject HKDF_AES_256 = CBORObject.FromObject(AlgorithmValuesInt.Direct_HKDF_AES_256);

        public static readonly CBORObject AES_GCM_128 = CBORObject.FromObject(AlgorithmValuesInt.AES_GCM_128);
        public static readonly CBORObject AES_GCM_192 = CBORObject.FromObject(AlgorithmValuesInt.AES_GCM_192);
        public static readonly CBORObject AES_GCM_256 = CBORObject.FromObject(AlgorithmValuesInt.AES_GCM_256);

        public static readonly CBORObject HMAC_SHA_256 = CBORObject.FromObject(AlgorithmValuesInt.HMAC_SHA_256);
        public static readonly CBORObject HMAC_SHA_384 = CBORObject.FromObject(AlgorithmValuesInt.HMAC_SHA_384);
        public static readonly CBORObject HMAC_SHA_512 = CBORObject.FromObject(AlgorithmValuesInt.HMAC_SHA_512);
        public static readonly CBORObject HMAC_SHA_256_64 = CBORObject.FromObject(AlgorithmValuesInt.HMAC_SHA_256_64);

        public static readonly CBORObject AES_CMAC_128_64 = CBORObject.FromObject("AES-CMAC-128/64");
        public static readonly CBORObject AES_CMAC_256_64 = CBORObject.FromObject("AES-CMAC-256/64");

        public static readonly CBORObject AES_CBC_MAC_128_64 = CBORObject.FromObject(AlgorithmValuesInt.AES_CBC_MAC_128_64);
        public static readonly CBORObject AES_CBC_MAC_256_64 = CBORObject.FromObject(AlgorithmValuesInt.AES_CBC_MAC_256_64);
        public static readonly CBORObject AES_CBC_MAC_128_128 = CBORObject.FromObject(AlgorithmValuesInt.AES_CBC_MAC_128_128);
        public static readonly CBORObject AES_CBC_MAC_256_128 = CBORObject.FromObject(AlgorithmValuesInt.AES_CBC_MAC_256_128);

        public static readonly CBORObject AES_CCM_16_64_128 = CBORObject.FromObject(AlgorithmValuesInt.AES_CCM_16_64_128);
        public static readonly CBORObject AES_CCM_16_128_128 = CBORObject.FromObject(AlgorithmValuesInt.AES_CCM_16_128_128);
        public static readonly CBORObject AES_CCM_16_64_256 = CBORObject.FromObject(AlgorithmValuesInt.AES_CCM_16_64_256);
        public static readonly CBORObject AES_CCM_16_128_256 = CBORObject.FromObject(AlgorithmValuesInt.AES_CCM_16_128_256);
        public static readonly CBORObject AES_CCM_64_64_128 = CBORObject.FromObject(AlgorithmValuesInt.AES_CCM_64_64_128);
        public static readonly CBORObject AES_CCM_64_128_128 = CBORObject.FromObject(AlgorithmValuesInt.AES_CCM_64_128_128);
        public static readonly CBORObject AES_CCM_64_64_256 = CBORObject.FromObject(AlgorithmValuesInt.AES_CCM_64_64_256);
        public static readonly CBORObject AES_CCM_64_128_256 = CBORObject.FromObject(AlgorithmValuesInt.AES_CCM_64_128_256);

        public static readonly CBORObject ChaCha20_Poly1305 = CBORObject.FromObject(AlgorithmValuesInt.ChaCha20_Poly1305);

        public static readonly CBORObject RSA_OAEP = CBORObject.FromObject(AlgorithmValuesInt.RSA_OAEP);
        public static readonly CBORObject RSA_OAEP_256 = CBORObject.FromObject(AlgorithmValuesInt.RSA_OAEP_256);

        public static readonly CBORObject AES_KW_128 = CBORObject.FromObject(AlgorithmValuesInt.AES_KW_128);
        public static readonly CBORObject AES_KW_192 = CBORObject.FromObject(AlgorithmValuesInt.AES_KW_192);
        public static readonly CBORObject AES_KW_256 = CBORObject.FromObject(AlgorithmValuesInt.AES_KW_256);

        public static readonly CBORObject Direct = CBORObject.FromObject(AlgorithmValuesInt.DIRECT);
        public static readonly CBORObject dir_kdf = CBORObject.FromObject("dir+kdf");

        public static readonly CBORObject ECDSA_256 = CBORObject.FromObject(AlgorithmValuesInt.ECDSA_256);
        public static readonly CBORObject ECDSA_384 = CBORObject.FromObject(AlgorithmValuesInt.ECDSA_384);
        public static readonly CBORObject ECDSA_512 = CBORObject.FromObject(AlgorithmValuesInt.ECDSA_512);

        public static readonly CBORObject EdDSA = CBORObject.FromObject(AlgorithmValuesInt.EdDSA);

        public static readonly CBORObject RSA_PSS_256 = CBORObject.FromObject(AlgorithmValuesInt.RSA_PSS_256);
        public static readonly CBORObject RSA_PSS_512 = CBORObject.FromObject(AlgorithmValuesInt.RSA_PSS_512);

        public static readonly CBORObject ECDH_ES_HKDF_256 = CBORObject.FromObject(AlgorithmValuesInt.ECDH_ES_HKDF_256);
        public static readonly CBORObject ECDH_SS_HKDF_256 = CBORObject.FromObject(AlgorithmValuesInt.ECDH_SS_HKDF_256);
        public static readonly CBORObject ECDH_ES_HKDF_512 = CBORObject.FromObject(AlgorithmValuesInt.ECDH_ES_HKDF_512);
        public static readonly CBORObject ECDH_SS_HKDF_512 = CBORObject.FromObject(AlgorithmValuesInt.ECDH_SS_HKDF_512);

        public static readonly CBORObject ECDH_ES_HKDF_256_AES_KW_128 = CBORObject.FromObject(AlgorithmValuesInt.ECDH_ES_HKDF_256_AES_KW_128);
        public static readonly CBORObject ECDH_ES_HKDF_256_AES_KW_192 = CBORObject.FromObject(AlgorithmValuesInt.ECDH_ES_HKDF_256_AES_KW_192);
        public static readonly CBORObject ECDH_ES_HKDF_256_AES_KW_256 = CBORObject.FromObject(AlgorithmValuesInt.ECDH_ES_HKDF_256_AES_KW_256);
        public static readonly CBORObject ECDH_SS_HKDF_256_AES_KW_128 = CBORObject.FromObject(AlgorithmValuesInt.ECDH_SS_HKDF_256_AES_KW_128);
        public static readonly CBORObject ECDH_SS_HKDF_256_AES_KW_192 = CBORObject.FromObject(AlgorithmValuesInt.ECDH_SS_HKDF_256_AES_KW_192);
        public static readonly CBORObject ECDH_SS_HKDF_256_AES_KW_256 = CBORObject.FromObject(AlgorithmValuesInt.ECDH_SS_HKDF_256_AES_KW_256);
    }

    public class CoseKeyKeys
    {
        public static readonly CBORObject KeyType = CBORObject.FromObject(1);
        public static readonly CBORObject KeyIdentifier = CBORObject.FromObject(2);
        public static readonly CBORObject Algorithm = CBORObject.FromObject(3);
        public static readonly CBORObject Key_Operations = CBORObject.FromObject(4);
        public static readonly CBORObject x5u = CBORObject.FromObject("x5u");
        public static readonly CBORObject x5c = CBORObject.FromObject("x5c");
        public static readonly CBORObject x5t = CBORObject.FromObject("x5t");
        public static readonly CBORObject x5t_sha_256 = CBORObject.FromObject("x5t#S256");
    }

    public class CoseKeyParameterKeys
    {
        public static readonly CBORObject EC_Curve = CBORObject.FromObject(-1);
        public static readonly CBORObject EC_X = CBORObject.FromObject(-2);
        public static readonly CBORObject EC_Y = CBORObject.FromObject(-3);
        public static readonly CBORObject EC_D = CBORObject.FromObject(-4);

        public static readonly CBORObject OKP_Curve = CBORObject.FromObject(-1);
        public static readonly CBORObject OKP_X = CBORObject.FromObject(-2);
        public static readonly CBORObject OKP_D = CBORObject.FromObject(-4);

        public static readonly CBORObject RSA_e = CBORObject.FromObject(-1);
        public static readonly CBORObject RSA_n = CBORObject.FromObject(-2);
        public static readonly CBORObject RSA_d = CBORObject.FromObject(-3);
        public static readonly CBORObject RSA_p = CBORObject.FromObject(-4);
        public static readonly CBORObject RSA_q = CBORObject.FromObject(-5);
        public static readonly CBORObject RSA_dP = CBORObject.FromObject(-6);
        public static readonly CBORObject RSA_dQ = CBORObject.FromObject(-7);
        public static readonly CBORObject RSA_qInv = CBORObject.FromObject(-8);

        public static readonly CBORObject Octet_k = CBORObject.FromObject(-1);

        public static readonly CBORObject ECDH_EPK = CBORObject.FromObject(-1);
        public static readonly CBORObject ECDH_StaticKey = CBORObject.FromObject(-2);
        public static readonly CBORObject ECDH_StaticKey_kid = CBORObject.FromObject(-3);

        public static readonly CBORObject HKDF_Salt = CBORObject.FromObject(-20);
        public static readonly CBORObject HKDF_Context_PartyU_ID = CBORObject.FromObject(-21);
        public static readonly CBORObject HKDF_Context_PartyU_nonce = CBORObject.FromObject(-22);
        public static readonly CBORObject HKDF_Context_PartyU_Other = CBORObject.FromObject(-23);
        public static readonly CBORObject HKDF_Context_PartyV_ID = CBORObject.FromObject(-24);
        public static readonly CBORObject HKDF_Context_PartyV_nonce = CBORObject.FromObject(-25);
        public static readonly CBORObject HKDF_Context_PartyV_Other = CBORObject.FromObject(-26);
        public static readonly CBORObject HKDF_SuppPub_Other = CBORObject.FromObject("HKDF Supp Public");
        public static readonly CBORObject HKDF_SuppPriv_Other = CBORObject.FromObject("HKDF Supp Private");

    }

    public enum GeneralValuesInt
    {
        KeyType_OKP = 1, KeyType_EC2=2, KeyType_RSA=3, KeyType_Octet = 4,
        P256=1, P384=2, P521=3, X25519=4, X448=5, Ed25519=6, Ed448=7
    }

    public class GeneralValues
    {
        public static readonly CBORObject KeyType_OKP = CBORObject.FromObject(GeneralValuesInt.KeyType_OKP);
        public static readonly CBORObject KeyType_EC = CBORObject.FromObject(GeneralValuesInt.KeyType_EC2);
        public static readonly CBORObject KeyType_RSA = CBORObject.FromObject(GeneralValuesInt.KeyType_RSA);
        public static readonly CBORObject KeyType_Octet = CBORObject.FromObject(GeneralValuesInt.KeyType_Octet);
        public static readonly CBORObject P256 = CBORObject.FromObject(GeneralValuesInt.P256);
        public static readonly CBORObject P384 = CBORObject.FromObject(GeneralValuesInt.P384);
        public static readonly CBORObject P521 = CBORObject.FromObject(GeneralValuesInt.P521);
        public static readonly CBORObject X25519 = CBORObject.FromObject(GeneralValuesInt.X25519);
        public static readonly CBORObject X448 = CBORObject.FromObject(GeneralValuesInt.X448);
        public static readonly CBORObject Ed25519 = CBORObject.FromObject(GeneralValuesInt.Ed25519);
        public static readonly CBORObject Ed448 = CBORObject.FromObject(GeneralValuesInt.Ed448);
    }

    public abstract class Message : Attributes
    {
        protected bool m_forceArray = true;
        protected List<CounterSignature> m_counterSignerList = new List<CounterSignature>();
        protected static SecureRandom s_PRNG = new SecureRandom();
        protected bool m_emitTag = true;
        protected bool m_emitContent;
        protected Tags m_tag;
        protected byte[] rgbContent;

        public Message(Boolean fEmitTag, Boolean fEmitContent)
        {
            m_emitTag = fEmitTag;
            m_emitContent = fEmitContent;
        }

        public static SecureRandom GetPRNG()
        {
            return s_PRNG;
        }

        public static void SetPRNG(SecureRandom prng)
        {
            s_PRNG = prng;
        }


        /// <summary>
        /// Given a byte array, decode and return the correct COSE message object.
        /// Message type can be provided explicitly or inferred from the CBOR tag element.
        /// If the explicit and inferred elements provide different answers, then it fails.
        /// </summary>
        /// <param name="messageData"></param>
        /// <param name="defaultTag"></param>
        /// <returns></returns>
        public static Message DecodeFromBytes(byte[] messageData, Tags defaultTag = Tags.Unknown)
        {
            CBORObject messageObject = CBORObject.DecodeFromBytes(messageData);

            return DecodeFromCBOR(messageObject, defaultTag);
        }

        /// <summary>
        /// Given a CBOR tree, decode and return the correct COSE message object.
        /// Message type can be provided explicitly or inferred from the CBOR tag element.
        /// If the explicit and inferred elements provide different answers, then it fails.
        /// </summary>
        /// <param name="messageObject"></param>
        /// <param name="defaultTag"></param>
        /// <returns></returns>
        public static Message DecodeFromCBOR(CBORObject messageObject, Tags defaultTag = Tags.Unknown)
        { 
            if (messageObject.Type != CBORType.Array) throw new CoseException("Message is not a COSE security message.");

            if (messageObject.IsTagged) {
                if (messageObject.GetTags().Count() != 1) throw new CoseException("Malformed message - too many tags");

                if (defaultTag == Tags.Unknown) {
#if NETSTD10
                    defaultTag = (Tags) messageObject.MostOuterTag.ToInt32Checked();
#else
                    defaultTag = (Tags) messageObject.OutermostTag.intValue();
#endif
                }
#if NETSTD10
                else if (defaultTag != (Tags) messageObject.MostOuterTag.ToInt32Checked()) {
                    throw new CoseException("Passed in tag does not match actual tag");
                }
#else
                else if (defaultTag != (Tags) messageObject.OutermostTag.intValue()) {
                    throw new CoseException("Passed in tag does not match actual tag");
                }
#endif
            }

            switch (defaultTag) {
            case Tags.Unknown:
                throw new CoseException("Message was not tagged and no default tagging option given");

            case Tags.Signed:
                SignMessage sig = new SignMessage();
                sig.DecodeFromCBORObject(messageObject);
                return sig;

            case Tags.Sign1:
                Sign1Message sig0 = new Sign1Message();
                sig0.DecodeFromCBORObject(messageObject);
                return sig0;
            
            case Tags.MAC: {
                    MACMessage mac = new MACMessage();
                    mac.DecodeFromCBORObject(messageObject);
                    return mac;
                }

            case Tags.MAC0:
                MAC0Message mac0 = new MAC0Message();
                mac0.DecodeFromCBORObject(messageObject);
                return mac0;

            case Tags.Encrypt:         // It is an encrytion message
                EncryptMessage enc = new EncryptMessage();

                enc.DecodeFromCBORObject(messageObject);
                return enc;

            case Tags.Encrypt0:
                Encrypt0Message enc0 = new Encrypt0Message();
                enc0.DecodeFromCBORObject(messageObject);
                return enc0;

            default:
                throw new CoseException("Message is not recognized as a COSE security message.");
            }
        }

        public byte[] EncodeToBytes()
        {
            CBORObject obj3;

            obj3 = EncodeToCBORObject();

            return obj3.EncodeToBytes();
        }

        [Obsolete]
        public void ForceArray(bool f)
        {
            m_forceArray = f;
        }

        public bool EmitTag
        {
            get { return m_emitTag; }
            set { m_emitTag = value; }
        }

        public void AddCounterSignature(Signer signer)
        {
            m_counterSignerList.Add((CounterSignature)signer);
        }

        public List<CounterSignature> CounterSignerList {  get { return m_counterSignerList; } }

        /// <summary>
        /// Generate a new CBOR Object based on the message.
        /// Doing this will force cryptographic operations to be created.
        /// </summary>
        /// <returns></returns>
        public CBORObject EncodeToCBORObject()
        {
            CBORObject obj = CBORObject.NewArray();
            CBORObject obj3 = Encode();

            for (int i = 0; i < obj3.Count; i++) obj.Add(obj3[i]);

            if (m_emitTag) return CBORObject.FromObjectAndTag(obj, (int) m_tag);
            return obj;
        }

        public abstract CBORObject Encode();

        public Boolean HasContent()
        {
            return rgbContent != null;
        }

        public byte[] GetContent() { return rgbContent; }
        public string GetContentAsString()
        {
            return Encoding.ASCII.GetString(rgbContent);
        }

        public void SetContent(byte[] contentIn) { rgbContent = contentIn; }
        public void SetContent(String contentString)
        {
            rgbContent = Encoding.ASCII.GetBytes(contentString);
        }
    }



 
        [Serializable]
    public class CoseException : Exception

    {
        public CoseException(string code) : base(code) { }
    }
}
