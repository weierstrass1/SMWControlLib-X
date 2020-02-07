using SMWControlLibUtils;

namespace SMWControlLibSNES.Enumerators.Graphics
{
    /// <summary>
    /// The g f x box size.
    /// </summary>
    public class GFXBoxSize : FakeEnumerator
    {
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }

        #region Values
        /// <summary>
        /// Gets the size8x8.
        /// </summary>
        public static GFXBoxSize Size8x8 { get { return getSize(8, 8); } }
        /// <summary>
        /// Gets the size8x16.
        /// </summary>
        public static GFXBoxSize Size8x16 { get { return getSize(8, 16); } }
        /// <summary>
        /// Gets the size8x24.
        /// </summary>
        public static GFXBoxSize Size8x24 { get { return getSize(8, 24); } }
        /// <summary>
        /// Gets the size8x32.
        /// </summary>
        public static GFXBoxSize Size8x32 { get { return getSize(8, 32); } }
        /// <summary>
        /// Gets the size8x40.
        /// </summary>
        public static GFXBoxSize Size8x40 { get { return getSize(8, 40); } }
        /// <summary>
        /// Gets the size8x48.
        /// </summary>
        public static GFXBoxSize Size8x48 { get { return getSize(8, 48); } }
        /// <summary>
        /// Gets the size8x56.
        /// </summary>
        public static GFXBoxSize Size8x56 { get { return getSize(8, 56); } }
        /// <summary>
        /// Gets the size8x64.
        /// </summary>
        public static GFXBoxSize Size8x64 { get { return getSize(8, 64); } }
        /// <summary>
        /// Gets the size8x72.
        /// </summary>
        public static GFXBoxSize Size8x72 { get { return getSize(8, 72); } }
        /// <summary>
        /// Gets the size8x80.
        /// </summary>
        public static GFXBoxSize Size8x80 { get { return getSize(8, 80); } }
        /// <summary>
        /// Gets the size8x88.
        /// </summary>
        public static GFXBoxSize Size8x88 { get { return getSize(8, 88); } }
        /// <summary>
        /// Gets the size8x96.
        /// </summary>
        public static GFXBoxSize Size8x96 { get { return getSize(8, 96); } }
        /// <summary>
        /// Gets the size8x104.
        /// </summary>
        public static GFXBoxSize Size8x104 { get { return getSize(8, 104); } }
        /// <summary>
        /// Gets the size8x112.
        /// </summary>
        public static GFXBoxSize Size8x112 { get { return getSize(8, 112); } }
        /// <summary>
        /// Gets the size8x120.
        /// </summary>
        public static GFXBoxSize Size8x120 { get { return getSize(8, 120); } }
        /// <summary>
        /// Gets the size8x128.
        /// </summary>
        public static GFXBoxSize Size8x128 { get { return getSize(8, 128); } }
        /// <summary>
        /// Gets the size16x8.
        /// </summary>
        public static GFXBoxSize Size16x8 { get { return getSize(16, 8); } }
        /// <summary>
        /// Gets the size16x16.
        /// </summary>
        public static GFXBoxSize Size16x16 { get { return getSize(16, 16); } }
        /// <summary>
        /// Gets the size16x24.
        /// </summary>
        public static GFXBoxSize Size16x24 { get { return getSize(16, 24); } }
        /// <summary>
        /// Gets the size16x32.
        /// </summary>
        public static GFXBoxSize Size16x32 { get { return getSize(16, 32); } }
        /// <summary>
        /// Gets the size16x40.
        /// </summary>
        public static GFXBoxSize Size16x40 { get { return getSize(16, 40); } }
        /// <summary>
        /// Gets the size16x48.
        /// </summary>
        public static GFXBoxSize Size16x48 { get { return getSize(16, 48); } }
        /// <summary>
        /// Gets the size16x56.
        /// </summary>
        public static GFXBoxSize Size16x56 { get { return getSize(16, 56); } }
        /// <summary>
        /// Gets the size16x64.
        /// </summary>
        public static GFXBoxSize Size16x64 { get { return getSize(16, 64); } }
        /// <summary>
        /// Gets the size16x72.
        /// </summary>
        public static GFXBoxSize Size16x72 { get { return getSize(16, 72); } }
        /// <summary>
        /// Gets the size16x80.
        /// </summary>
        public static GFXBoxSize Size16x80 { get { return getSize(16, 80); } }
        /// <summary>
        /// Gets the size16x88.
        /// </summary>
        public static GFXBoxSize Size16x88 { get { return getSize(16, 88); } }
        /// <summary>
        /// Gets the size16x96.
        /// </summary>
        public static GFXBoxSize Size16x96 { get { return getSize(16, 96); } }
        /// <summary>
        /// Gets the size16x104.
        /// </summary>
        public static GFXBoxSize Size16x104 { get { return getSize(16, 104); } }
        /// <summary>
        /// Gets the size16x112.
        /// </summary>
        public static GFXBoxSize Size16x112 { get { return getSize(16, 112); } }
        /// <summary>
        /// Gets the size16x120.
        /// </summary>
        public static GFXBoxSize Size16x120 { get { return getSize(16, 120); } }
        /// <summary>
        /// Gets the size16x128.
        /// </summary>
        public static GFXBoxSize Size16x128 { get { return getSize(16, 128); } }
        /// <summary>
        /// Gets the size24x8.
        /// </summary>
        public static GFXBoxSize Size24x8 { get { return getSize(24, 8); } }
        /// <summary>
        /// Gets the size24x16.
        /// </summary>
        public static GFXBoxSize Size24x16 { get { return getSize(24, 16); } }
        /// <summary>
        /// Gets the size24x24.
        /// </summary>
        public static GFXBoxSize Size24x24 { get { return getSize(24, 24); } }
        /// <summary>
        /// Gets the size24x32.
        /// </summary>
        public static GFXBoxSize Size24x32 { get { return getSize(24, 32); } }
        /// <summary>
        /// Gets the size24x40.
        /// </summary>
        public static GFXBoxSize Size24x40 { get { return getSize(24, 40); } }
        /// <summary>
        /// Gets the size24x48.
        /// </summary>
        public static GFXBoxSize Size24x48 { get { return getSize(24, 48); } }
        /// <summary>
        /// Gets the size24x56.
        /// </summary>
        public static GFXBoxSize Size24x56 { get { return getSize(24, 56); } }
        /// <summary>
        /// Gets the size24x64.
        /// </summary>
        public static GFXBoxSize Size24x64 { get { return getSize(24, 64); } }
        /// <summary>
        /// Gets the size24x72.
        /// </summary>
        public static GFXBoxSize Size24x72 { get { return getSize(24, 72); } }
        /// <summary>
        /// Gets the size24x80.
        /// </summary>
        public static GFXBoxSize Size24x80 { get { return getSize(24, 80); } }
        /// <summary>
        /// Gets the size24x88.
        /// </summary>
        public static GFXBoxSize Size24x88 { get { return getSize(24, 88); } }
        /// <summary>
        /// Gets the size24x96.
        /// </summary>
        public static GFXBoxSize Size24x96 { get { return getSize(24, 96); } }
        /// <summary>
        /// Gets the size24x104.
        /// </summary>
        public static GFXBoxSize Size24x104 { get { return getSize(24, 104); } }
        /// <summary>
        /// Gets the size24x112.
        /// </summary>
        public static GFXBoxSize Size24x112 { get { return getSize(24, 112); } }
        /// <summary>
        /// Gets the size24x120.
        /// </summary>
        public static GFXBoxSize Size24x120 { get { return getSize(24, 120); } }
        /// <summary>
        /// Gets the size24x128.
        /// </summary>
        public static GFXBoxSize Size24x128 { get { return getSize(24, 128); } }
        /// <summary>
        /// Gets the size32x8.
        /// </summary>
        public static GFXBoxSize Size32x8 { get { return getSize(32, 8); } }
        /// <summary>
        /// Gets the size32x16.
        /// </summary>
        public static GFXBoxSize Size32x16 { get { return getSize(32, 16); } }
        /// <summary>
        /// Gets the size32x24.
        /// </summary>
        public static GFXBoxSize Size32x24 { get { return getSize(32, 24); } }
        /// <summary>
        /// Gets the size32x32.
        /// </summary>
        public static GFXBoxSize Size32x32 { get { return getSize(32, 32); } }
        /// <summary>
        /// Gets the size32x40.
        /// </summary>
        public static GFXBoxSize Size32x40 { get { return getSize(32, 40); } }
        /// <summary>
        /// Gets the size32x48.
        /// </summary>
        public static GFXBoxSize Size32x48 { get { return getSize(32, 48); } }
        /// <summary>
        /// Gets the size32x56.
        /// </summary>
        public static GFXBoxSize Size32x56 { get { return getSize(32, 56); } }
        /// <summary>
        /// Gets the size32x64.
        /// </summary>
        public static GFXBoxSize Size32x64 { get { return getSize(32, 64); } }
        /// <summary>
        /// Gets the size32x72.
        /// </summary>
        public static GFXBoxSize Size32x72 { get { return getSize(32, 72); } }
        /// <summary>
        /// Gets the size32x80.
        /// </summary>
        public static GFXBoxSize Size32x80 { get { return getSize(32, 80); } }
        /// <summary>
        /// Gets the size32x88.
        /// </summary>
        public static GFXBoxSize Size32x88 { get { return getSize(32, 88); } }
        /// <summary>
        /// Gets the size32x96.
        /// </summary>
        public static GFXBoxSize Size32x96 { get { return getSize(32, 96); } }
        /// <summary>
        /// Gets the size32x104.
        /// </summary>
        public static GFXBoxSize Size32x104 { get { return getSize(32, 104); } }
        /// <summary>
        /// Gets the size32x112.
        /// </summary>
        public static GFXBoxSize Size32x112 { get { return getSize(32, 112); } }
        /// <summary>
        /// Gets the size32x120.
        /// </summary>
        public static GFXBoxSize Size32x120 { get { return getSize(32, 120); } }
        /// <summary>
        /// Gets the size32x128.
        /// </summary>
        public static GFXBoxSize Size32x128 { get { return getSize(32, 128); } }
        /// <summary>
        /// Gets the size40x8.
        /// </summary>
        public static GFXBoxSize Size40x8 { get { return getSize(40, 8); } }
        /// <summary>
        /// Gets the size40x16.
        /// </summary>
        public static GFXBoxSize Size40x16 { get { return getSize(40, 16); } }
        /// <summary>
        /// Gets the size40x24.
        /// </summary>
        public static GFXBoxSize Size40x24 { get { return getSize(40, 24); } }
        /// <summary>
        /// Gets the size40x32.
        /// </summary>
        public static GFXBoxSize Size40x32 { get { return getSize(40, 32); } }
        /// <summary>
        /// Gets the size40x40.
        /// </summary>
        public static GFXBoxSize Size40x40 { get { return getSize(40, 40); } }
        /// <summary>
        /// Gets the size40x48.
        /// </summary>
        public static GFXBoxSize Size40x48 { get { return getSize(40, 48); } }
        /// <summary>
        /// Gets the size40x56.
        /// </summary>
        public static GFXBoxSize Size40x56 { get { return getSize(40, 56); } }
        /// <summary>
        /// Gets the size40x64.
        /// </summary>
        public static GFXBoxSize Size40x64 { get { return getSize(40, 64); } }
        /// <summary>
        /// Gets the size40x72.
        /// </summary>
        public static GFXBoxSize Size40x72 { get { return getSize(40, 72); } }
        /// <summary>
        /// Gets the size40x80.
        /// </summary>
        public static GFXBoxSize Size40x80 { get { return getSize(40, 80); } }
        /// <summary>
        /// Gets the size40x88.
        /// </summary>
        public static GFXBoxSize Size40x88 { get { return getSize(40, 88); } }
        /// <summary>
        /// Gets the size40x96.
        /// </summary>
        public static GFXBoxSize Size40x96 { get { return getSize(40, 96); } }
        /// <summary>
        /// Gets the size40x104.
        /// </summary>
        public static GFXBoxSize Size40x104 { get { return getSize(40, 104); } }
        /// <summary>
        /// Gets the size40x112.
        /// </summary>
        public static GFXBoxSize Size40x112 { get { return getSize(40, 112); } }
        /// <summary>
        /// Gets the size40x120.
        /// </summary>
        public static GFXBoxSize Size40x120 { get { return getSize(40, 120); } }
        /// <summary>
        /// Gets the size40x128.
        /// </summary>
        public static GFXBoxSize Size40x128 { get { return getSize(40, 128); } }
        /// <summary>
        /// Gets the size48x8.
        /// </summary>
        public static GFXBoxSize Size48x8 { get { return getSize(48, 8); } }
        /// <summary>
        /// Gets the size48x16.
        /// </summary>
        public static GFXBoxSize Size48x16 { get { return getSize(48, 16); } }
        /// <summary>
        /// Gets the size48x24.
        /// </summary>
        public static GFXBoxSize Size48x24 { get { return getSize(48, 24); } }
        /// <summary>
        /// Gets the size48x32.
        /// </summary>
        public static GFXBoxSize Size48x32 { get { return getSize(48, 32); } }
        /// <summary>
        /// Gets the size48x40.
        /// </summary>
        public static GFXBoxSize Size48x40 { get { return getSize(48, 40); } }
        /// <summary>
        /// Gets the size48x48.
        /// </summary>
        public static GFXBoxSize Size48x48 { get { return getSize(48, 48); } }
        /// <summary>
        /// Gets the size48x56.
        /// </summary>
        public static GFXBoxSize Size48x56 { get { return getSize(48, 56); } }
        /// <summary>
        /// Gets the size48x64.
        /// </summary>
        public static GFXBoxSize Size48x64 { get { return getSize(48, 64); } }
        /// <summary>
        /// Gets the size48x72.
        /// </summary>
        public static GFXBoxSize Size48x72 { get { return getSize(48, 72); } }
        /// <summary>
        /// Gets the size48x80.
        /// </summary>
        public static GFXBoxSize Size48x80 { get { return getSize(48, 80); } }
        /// <summary>
        /// Gets the size48x88.
        /// </summary>
        public static GFXBoxSize Size48x88 { get { return getSize(48, 88); } }
        /// <summary>
        /// Gets the size48x96.
        /// </summary>
        public static GFXBoxSize Size48x96 { get { return getSize(48, 96); } }
        /// <summary>
        /// Gets the size48x104.
        /// </summary>
        public static GFXBoxSize Size48x104 { get { return getSize(48, 104); } }
        /// <summary>
        /// Gets the size48x112.
        /// </summary>
        public static GFXBoxSize Size48x112 { get { return getSize(48, 112); } }
        /// <summary>
        /// Gets the size48x120.
        /// </summary>
        public static GFXBoxSize Size48x120 { get { return getSize(48, 120); } }
        /// <summary>
        /// Gets the size48x128.
        /// </summary>
        public static GFXBoxSize Size48x128 { get { return getSize(48, 128); } }
        /// <summary>
        /// Gets the size56x8.
        /// </summary>
        public static GFXBoxSize Size56x8 { get { return getSize(56, 8); } }
        /// <summary>
        /// Gets the size56x16.
        /// </summary>
        public static GFXBoxSize Size56x16 { get { return getSize(56, 16); } }
        /// <summary>
        /// Gets the size56x24.
        /// </summary>
        public static GFXBoxSize Size56x24 { get { return getSize(56, 24); } }
        /// <summary>
        /// Gets the size56x32.
        /// </summary>
        public static GFXBoxSize Size56x32 { get { return getSize(56, 32); } }
        /// <summary>
        /// Gets the size56x40.
        /// </summary>
        public static GFXBoxSize Size56x40 { get { return getSize(56, 40); } }
        /// <summary>
        /// Gets the size56x48.
        /// </summary>
        public static GFXBoxSize Size56x48 { get { return getSize(56, 48); } }
        /// <summary>
        /// Gets the size56x56.
        /// </summary>
        public static GFXBoxSize Size56x56 { get { return getSize(56, 56); } }
        /// <summary>
        /// Gets the size56x64.
        /// </summary>
        public static GFXBoxSize Size56x64 { get { return getSize(56, 64); } }
        /// <summary>
        /// Gets the size56x72.
        /// </summary>
        public static GFXBoxSize Size56x72 { get { return getSize(56, 72); } }
        /// <summary>
        /// Gets the size56x80.
        /// </summary>
        public static GFXBoxSize Size56x80 { get { return getSize(56, 80); } }
        /// <summary>
        /// Gets the size56x88.
        /// </summary>
        public static GFXBoxSize Size56x88 { get { return getSize(56, 88); } }
        /// <summary>
        /// Gets the size56x96.
        /// </summary>
        public static GFXBoxSize Size56x96 { get { return getSize(56, 96); } }
        /// <summary>
        /// Gets the size56x104.
        /// </summary>
        public static GFXBoxSize Size56x104 { get { return getSize(56, 104); } }
        /// <summary>
        /// Gets the size56x112.
        /// </summary>
        public static GFXBoxSize Size56x112 { get { return getSize(56, 112); } }
        /// <summary>
        /// Gets the size56x120.
        /// </summary>
        public static GFXBoxSize Size56x120 { get { return getSize(56, 120); } }
        /// <summary>
        /// Gets the size56x128.
        /// </summary>
        public static GFXBoxSize Size56x128 { get { return getSize(56, 128); } }
        /// <summary>
        /// Gets the size64x8.
        /// </summary>
        public static GFXBoxSize Size64x8 { get { return getSize(64, 8); } }
        /// <summary>
        /// Gets the size64x16.
        /// </summary>
        public static GFXBoxSize Size64x16 { get { return getSize(64, 16); } }
        /// <summary>
        /// Gets the size64x24.
        /// </summary>
        public static GFXBoxSize Size64x24 { get { return getSize(64, 24); } }
        /// <summary>
        /// Gets the size64x32.
        /// </summary>
        public static GFXBoxSize Size64x32 { get { return getSize(64, 32); } }
        /// <summary>
        /// Gets the size64x40.
        /// </summary>
        public static GFXBoxSize Size64x40 { get { return getSize(64, 40); } }
        /// <summary>
        /// Gets the size64x48.
        /// </summary>
        public static GFXBoxSize Size64x48 { get { return getSize(64, 48); } }
        /// <summary>
        /// Gets the size64x56.
        /// </summary>
        public static GFXBoxSize Size64x56 { get { return getSize(64, 56); } }
        /// <summary>
        /// Gets the size64x64.
        /// </summary>
        public static GFXBoxSize Size64x64 { get { return getSize(64, 64); } }
        /// <summary>
        /// Gets the size64x72.
        /// </summary>
        public static GFXBoxSize Size64x72 { get { return getSize(64, 72); } }
        /// <summary>
        /// Gets the size64x80.
        /// </summary>
        public static GFXBoxSize Size64x80 { get { return getSize(64, 80); } }
        /// <summary>
        /// Gets the size64x88.
        /// </summary>
        public static GFXBoxSize Size64x88 { get { return getSize(64, 88); } }
        /// <summary>
        /// Gets the size64x96.
        /// </summary>
        public static GFXBoxSize Size64x96 { get { return getSize(64, 96); } }
        /// <summary>
        /// Gets the size64x104.
        /// </summary>
        public static GFXBoxSize Size64x104 { get { return getSize(64, 104); } }
        /// <summary>
        /// Gets the size64x112.
        /// </summary>
        public static GFXBoxSize Size64x112 { get { return getSize(64, 112); } }
        /// <summary>
        /// Gets the size64x120.
        /// </summary>
        public static GFXBoxSize Size64x120 { get { return getSize(64, 120); } }
        /// <summary>
        /// Gets the size64x128.
        /// </summary>
        public static GFXBoxSize Size64x128 { get { return getSize(64, 128); } }
        /// <summary>
        /// Gets the size72x8.
        /// </summary>
        public static GFXBoxSize Size72x8 { get { return getSize(72, 8); } }
        /// <summary>
        /// Gets the size72x16.
        /// </summary>
        public static GFXBoxSize Size72x16 { get { return getSize(72, 16); } }
        /// <summary>
        /// Gets the size72x24.
        /// </summary>
        public static GFXBoxSize Size72x24 { get { return getSize(72, 24); } }
        /// <summary>
        /// Gets the size72x32.
        /// </summary>
        public static GFXBoxSize Size72x32 { get { return getSize(72, 32); } }
        /// <summary>
        /// Gets the size72x40.
        /// </summary>
        public static GFXBoxSize Size72x40 { get { return getSize(72, 40); } }
        /// <summary>
        /// Gets the size72x48.
        /// </summary>
        public static GFXBoxSize Size72x48 { get { return getSize(72, 48); } }
        /// <summary>
        /// Gets the size72x56.
        /// </summary>
        public static GFXBoxSize Size72x56 { get { return getSize(72, 56); } }
        /// <summary>
        /// Gets the size72x64.
        /// </summary>
        public static GFXBoxSize Size72x64 { get { return getSize(72, 64); } }
        /// <summary>
        /// Gets the size72x72.
        /// </summary>
        public static GFXBoxSize Size72x72 { get { return getSize(72, 72); } }
        /// <summary>
        /// Gets the size72x80.
        /// </summary>
        public static GFXBoxSize Size72x80 { get { return getSize(72, 80); } }
        /// <summary>
        /// Gets the size72x88.
        /// </summary>
        public static GFXBoxSize Size72x88 { get { return getSize(72, 88); } }
        /// <summary>
        /// Gets the size72x96.
        /// </summary>
        public static GFXBoxSize Size72x96 { get { return getSize(72, 96); } }
        /// <summary>
        /// Gets the size72x104.
        /// </summary>
        public static GFXBoxSize Size72x104 { get { return getSize(72, 104); } }
        /// <summary>
        /// Gets the size72x112.
        /// </summary>
        public static GFXBoxSize Size72x112 { get { return getSize(72, 112); } }
        /// <summary>
        /// Gets the size72x120.
        /// </summary>
        public static GFXBoxSize Size72x120 { get { return getSize(72, 120); } }
        /// <summary>
        /// Gets the size72x128.
        /// </summary>
        public static GFXBoxSize Size72x128 { get { return getSize(72, 128); } }
        /// <summary>
        /// Gets the size80x8.
        /// </summary>
        public static GFXBoxSize Size80x8 { get { return getSize(80, 8); } }
        /// <summary>
        /// Gets the size80x16.
        /// </summary>
        public static GFXBoxSize Size80x16 { get { return getSize(80, 16); } }
        /// <summary>
        /// Gets the size80x24.
        /// </summary>
        public static GFXBoxSize Size80x24 { get { return getSize(80, 24); } }
        /// <summary>
        /// Gets the size80x32.
        /// </summary>
        public static GFXBoxSize Size80x32 { get { return getSize(80, 32); } }
        /// <summary>
        /// Gets the size80x40.
        /// </summary>
        public static GFXBoxSize Size80x40 { get { return getSize(80, 40); } }
        /// <summary>
        /// Gets the size80x48.
        /// </summary>
        public static GFXBoxSize Size80x48 { get { return getSize(80, 48); } }
        /// <summary>
        /// Gets the size80x56.
        /// </summary>
        public static GFXBoxSize Size80x56 { get { return getSize(80, 56); } }
        /// <summary>
        /// Gets the size80x64.
        /// </summary>
        public static GFXBoxSize Size80x64 { get { return getSize(80, 64); } }
        /// <summary>
        /// Gets the size80x72.
        /// </summary>
        public static GFXBoxSize Size80x72 { get { return getSize(80, 72); } }
        /// <summary>
        /// Gets the size80x80.
        /// </summary>
        public static GFXBoxSize Size80x80 { get { return getSize(80, 80); } }
        /// <summary>
        /// Gets the size80x88.
        /// </summary>
        public static GFXBoxSize Size80x88 { get { return getSize(80, 88); } }
        /// <summary>
        /// Gets the size80x96.
        /// </summary>
        public static GFXBoxSize Size80x96 { get { return getSize(80, 96); } }
        /// <summary>
        /// Gets the size80x104.
        /// </summary>
        public static GFXBoxSize Size80x104 { get { return getSize(80, 104); } }
        /// <summary>
        /// Gets the size80x112.
        /// </summary>
        public static GFXBoxSize Size80x112 { get { return getSize(80, 112); } }
        /// <summary>
        /// Gets the size80x120.
        /// </summary>
        public static GFXBoxSize Size80x120 { get { return getSize(80, 120); } }
        /// <summary>
        /// Gets the size80x128.
        /// </summary>
        public static GFXBoxSize Size80x128 { get { return getSize(80, 128); } }
        /// <summary>
        /// Gets the size88x8.
        /// </summary>
        public static GFXBoxSize Size88x8 { get { return getSize(88, 8); } }
        /// <summary>
        /// Gets the size88x16.
        /// </summary>
        public static GFXBoxSize Size88x16 { get { return getSize(88, 16); } }
        /// <summary>
        /// Gets the size88x24.
        /// </summary>
        public static GFXBoxSize Size88x24 { get { return getSize(88, 24); } }
        /// <summary>
        /// Gets the size88x32.
        /// </summary>
        public static GFXBoxSize Size88x32 { get { return getSize(88, 32); } }
        /// <summary>
        /// Gets the size88x40.
        /// </summary>
        public static GFXBoxSize Size88x40 { get { return getSize(88, 40); } }
        /// <summary>
        /// Gets the size88x48.
        /// </summary>
        public static GFXBoxSize Size88x48 { get { return getSize(88, 48); } }
        /// <summary>
        /// Gets the size88x56.
        /// </summary>
        public static GFXBoxSize Size88x56 { get { return getSize(88, 56); } }
        /// <summary>
        /// Gets the size88x64.
        /// </summary>
        public static GFXBoxSize Size88x64 { get { return getSize(88, 64); } }
        /// <summary>
        /// Gets the size88x72.
        /// </summary>
        public static GFXBoxSize Size88x72 { get { return getSize(88, 72); } }
        /// <summary>
        /// Gets the size88x80.
        /// </summary>
        public static GFXBoxSize Size88x80 { get { return getSize(88, 80); } }
        /// <summary>
        /// Gets the size88x88.
        /// </summary>
        public static GFXBoxSize Size88x88 { get { return getSize(88, 88); } }
        /// <summary>
        /// Gets the size88x96.
        /// </summary>
        public static GFXBoxSize Size88x96 { get { return getSize(88, 96); } }
        /// <summary>
        /// Gets the size88x104.
        /// </summary>
        public static GFXBoxSize Size88x104 { get { return getSize(88, 104); } }
        /// <summary>
        /// Gets the size88x112.
        /// </summary>
        public static GFXBoxSize Size88x112 { get { return getSize(88, 112); } }
        /// <summary>
        /// Gets the size88x120.
        /// </summary>
        public static GFXBoxSize Size88x120 { get { return getSize(88, 120); } }
        /// <summary>
        /// Gets the size88x128.
        /// </summary>
        public static GFXBoxSize Size88x128 { get { return getSize(88, 128); } }
        /// <summary>
        /// Gets the size96x8.
        /// </summary>
        public static GFXBoxSize Size96x8 { get { return getSize(96, 8); } }
        /// <summary>
        /// Gets the size96x16.
        /// </summary>
        public static GFXBoxSize Size96x16 { get { return getSize(96, 16); } }
        /// <summary>
        /// Gets the size96x24.
        /// </summary>
        public static GFXBoxSize Size96x24 { get { return getSize(96, 24); } }
        /// <summary>
        /// Gets the size96x32.
        /// </summary>
        public static GFXBoxSize Size96x32 { get { return getSize(96, 32); } }
        /// <summary>
        /// Gets the size96x40.
        /// </summary>
        public static GFXBoxSize Size96x40 { get { return getSize(96, 40); } }
        /// <summary>
        /// Gets the size96x48.
        /// </summary>
        public static GFXBoxSize Size96x48 { get { return getSize(96, 48); } }
        /// <summary>
        /// Gets the size96x56.
        /// </summary>
        public static GFXBoxSize Size96x56 { get { return getSize(96, 56); } }
        /// <summary>
        /// Gets the size96x64.
        /// </summary>
        public static GFXBoxSize Size96x64 { get { return getSize(96, 64); } }
        /// <summary>
        /// Gets the size96x72.
        /// </summary>
        public static GFXBoxSize Size96x72 { get { return getSize(96, 72); } }
        /// <summary>
        /// Gets the size96x80.
        /// </summary>
        public static GFXBoxSize Size96x80 { get { return getSize(96, 80); } }
        /// <summary>
        /// Gets the size96x88.
        /// </summary>
        public static GFXBoxSize Size96x88 { get { return getSize(96, 88); } }
        /// <summary>
        /// Gets the size96x96.
        /// </summary>
        public static GFXBoxSize Size96x96 { get { return getSize(96, 96); } }
        /// <summary>
        /// Gets the size96x104.
        /// </summary>
        public static GFXBoxSize Size96x104 { get { return getSize(96, 104); } }
        /// <summary>
        /// Gets the size96x112.
        /// </summary>
        public static GFXBoxSize Size96x112 { get { return getSize(96, 112); } }
        /// <summary>
        /// Gets the size96x120.
        /// </summary>
        public static GFXBoxSize Size96x120 { get { return getSize(96, 120); } }
        /// <summary>
        /// Gets the size96x128.
        /// </summary>
        public static GFXBoxSize Size96x128 { get { return getSize(96, 128); } }
        /// <summary>
        /// Gets the size104x8.
        /// </summary>
        public static GFXBoxSize Size104x8 { get { return getSize(104, 8); } }
        /// <summary>
        /// Gets the size104x16.
        /// </summary>
        public static GFXBoxSize Size104x16 { get { return getSize(104, 16); } }
        /// <summary>
        /// Gets the size104x24.
        /// </summary>
        public static GFXBoxSize Size104x24 { get { return getSize(104, 24); } }
        /// <summary>
        /// Gets the size104x32.
        /// </summary>
        public static GFXBoxSize Size104x32 { get { return getSize(104, 32); } }
        /// <summary>
        /// Gets the size104x40.
        /// </summary>
        public static GFXBoxSize Size104x40 { get { return getSize(104, 40); } }
        /// <summary>
        /// Gets the size104x48.
        /// </summary>
        public static GFXBoxSize Size104x48 { get { return getSize(104, 48); } }
        /// <summary>
        /// Gets the size104x56.
        /// </summary>
        public static GFXBoxSize Size104x56 { get { return getSize(104, 56); } }
        /// <summary>
        /// Gets the size104x64.
        /// </summary>
        public static GFXBoxSize Size104x64 { get { return getSize(104, 64); } }
        /// <summary>
        /// Gets the size104x72.
        /// </summary>
        public static GFXBoxSize Size104x72 { get { return getSize(104, 72); } }
        /// <summary>
        /// Gets the size104x80.
        /// </summary>
        public static GFXBoxSize Size104x80 { get { return getSize(104, 80); } }
        /// <summary>
        /// Gets the size104x88.
        /// </summary>
        public static GFXBoxSize Size104x88 { get { return getSize(104, 88); } }
        /// <summary>
        /// Gets the size104x96.
        /// </summary>
        public static GFXBoxSize Size104x96 { get { return getSize(104, 96); } }
        /// <summary>
        /// Gets the size104x104.
        /// </summary>
        public static GFXBoxSize Size104x104 { get { return getSize(104, 104); } }
        /// <summary>
        /// Gets the size104x112.
        /// </summary>
        public static GFXBoxSize Size104x112 { get { return getSize(104, 112); } }
        /// <summary>
        /// Gets the size104x120.
        /// </summary>
        public static GFXBoxSize Size104x120 { get { return getSize(104, 120); } }
        /// <summary>
        /// Gets the size104x128.
        /// </summary>
        public static GFXBoxSize Size104x128 { get { return getSize(104, 128); } }
        /// <summary>
        /// Gets the size112x8.
        /// </summary>
        public static GFXBoxSize Size112x8 { get { return getSize(112, 8); } }
        /// <summary>
        /// Gets the size112x16.
        /// </summary>
        public static GFXBoxSize Size112x16 { get { return getSize(112, 16); } }
        /// <summary>
        /// Gets the size112x24.
        /// </summary>
        public static GFXBoxSize Size112x24 { get { return getSize(112, 24); } }
        /// <summary>
        /// Gets the size112x32.
        /// </summary>
        public static GFXBoxSize Size112x32 { get { return getSize(112, 32); } }
        /// <summary>
        /// Gets the size112x40.
        /// </summary>
        public static GFXBoxSize Size112x40 { get { return getSize(112, 40); } }
        /// <summary>
        /// Gets the size112x48.
        /// </summary>
        public static GFXBoxSize Size112x48 { get { return getSize(112, 48); } }
        /// <summary>
        /// Gets the size112x56.
        /// </summary>
        public static GFXBoxSize Size112x56 { get { return getSize(112, 56); } }
        /// <summary>
        /// Gets the size112x64.
        /// </summary>
        public static GFXBoxSize Size112x64 { get { return getSize(112, 64); } }
        /// <summary>
        /// Gets the size112x72.
        /// </summary>
        public static GFXBoxSize Size112x72 { get { return getSize(112, 72); } }
        /// <summary>
        /// Gets the size112x80.
        /// </summary>
        public static GFXBoxSize Size112x80 { get { return getSize(112, 80); } }
        /// <summary>
        /// Gets the size112x88.
        /// </summary>
        public static GFXBoxSize Size112x88 { get { return getSize(112, 88); } }
        /// <summary>
        /// Gets the size112x96.
        /// </summary>
        public static GFXBoxSize Size112x96 { get { return getSize(112, 96); } }
        /// <summary>
        /// Gets the size112x104.
        /// </summary>
        public static GFXBoxSize Size112x104 { get { return getSize(112, 104); } }
        /// <summary>
        /// Gets the size112x112.
        /// </summary>
        public static GFXBoxSize Size112x112 { get { return getSize(112, 112); } }
        /// <summary>
        /// Gets the size112x120.
        /// </summary>
        public static GFXBoxSize Size112x120 { get { return getSize(112, 120); } }
        /// <summary>
        /// Gets the size112x128.
        /// </summary>
        public static GFXBoxSize Size112x128 { get { return getSize(112, 128); } }
        /// <summary>
        /// Gets the size120x8.
        /// </summary>
        public static GFXBoxSize Size120x8 { get { return getSize(120, 8); } }
        /// <summary>
        /// Gets the size120x16.
        /// </summary>
        public static GFXBoxSize Size120x16 { get { return getSize(120, 16); } }
        /// <summary>
        /// Gets the size120x24.
        /// </summary>
        public static GFXBoxSize Size120x24 { get { return getSize(120, 24); } }
        /// <summary>
        /// Gets the size120x32.
        /// </summary>
        public static GFXBoxSize Size120x32 { get { return getSize(120, 32); } }
        /// <summary>
        /// Gets the size120x40.
        /// </summary>
        public static GFXBoxSize Size120x40 { get { return getSize(120, 40); } }
        /// <summary>
        /// Gets the size120x48.
        /// </summary>
        public static GFXBoxSize Size120x48 { get { return getSize(120, 48); } }
        /// <summary>
        /// Gets the size120x56.
        /// </summary>
        public static GFXBoxSize Size120x56 { get { return getSize(120, 56); } }
        /// <summary>
        /// Gets the size120x64.
        /// </summary>
        public static GFXBoxSize Size120x64 { get { return getSize(120, 64); } }
        /// <summary>
        /// Gets the size120x72.
        /// </summary>
        public static GFXBoxSize Size120x72 { get { return getSize(120, 72); } }
        /// <summary>
        /// Gets the size120x80.
        /// </summary>
        public static GFXBoxSize Size120x80 { get { return getSize(120, 80); } }
        /// <summary>
        /// Gets the size120x88.
        /// </summary>
        public static GFXBoxSize Size120x88 { get { return getSize(120, 88); } }
        /// <summary>
        /// Gets the size120x96.
        /// </summary>
        public static GFXBoxSize Size120x96 { get { return getSize(120, 96); } }
        /// <summary>
        /// Gets the size120x104.
        /// </summary>
        public static GFXBoxSize Size120x104 { get { return getSize(120, 104); } }
        /// <summary>
        /// Gets the size120x112.
        /// </summary>
        public static GFXBoxSize Size120x112 { get { return getSize(120, 112); } }
        /// <summary>
        /// Gets the size120x120.
        /// </summary>
        public static GFXBoxSize Size120x120 { get { return getSize(120, 120); } }
        /// <summary>
        /// Gets the size120x128.
        /// </summary>
        public static GFXBoxSize Size120x128 { get { return getSize(120, 128); } }
        /// <summary>
        /// Gets the size128x8.
        /// </summary>
        public static GFXBoxSize Size128x8 { get { return getSize(128, 8); } }
        /// <summary>
        /// Gets the size128x16.
        /// </summary>
        public static GFXBoxSize Size128x16 { get { return getSize(128, 16); } }
        /// <summary>
        /// Gets the size128x24.
        /// </summary>
        public static GFXBoxSize Size128x24 { get { return getSize(128, 24); } }
        /// <summary>
        /// Gets the size128x32.
        /// </summary>
        public static GFXBoxSize Size128x32 { get { return getSize(128, 32); } }
        /// <summary>
        /// Gets the size128x40.
        /// </summary>
        public static GFXBoxSize Size128x40 { get { return getSize(128, 40); } }
        /// <summary>
        /// Gets the size128x48.
        /// </summary>
        public static GFXBoxSize Size128x48 { get { return getSize(128, 48); } }
        /// <summary>
        /// Gets the size128x56.
        /// </summary>
        public static GFXBoxSize Size128x56 { get { return getSize(128, 56); } }
        /// <summary>
        /// Gets the size128x64.
        /// </summary>
        public static GFXBoxSize Size128x64 { get { return getSize(128, 64); } }
        /// <summary>
        /// Gets the size128x72.
        /// </summary>
        public static GFXBoxSize Size128x72 { get { return getSize(128, 72); } }
        /// <summary>
        /// Gets the size128x80.
        /// </summary>
        public static GFXBoxSize Size128x80 { get { return getSize(128, 80); } }
        /// <summary>
        /// Gets the size128x88.
        /// </summary>
        public static GFXBoxSize Size128x88 { get { return getSize(128, 88); } }
        /// <summary>
        /// Gets the size128x96.
        /// </summary>
        public static GFXBoxSize Size128x96 { get { return getSize(128, 96); } }
        /// <summary>
        /// Gets the size128x104.
        /// </summary>
        public static GFXBoxSize Size128x104 { get { return getSize(128, 104); } }
        /// <summary>
        /// Gets the size128x112.
        /// </summary>
        public static GFXBoxSize Size128x112 { get { return getSize(128, 112); } }
        /// <summary>
        /// Gets the size128x120.
        /// </summary>
        public static GFXBoxSize Size128x120 { get { return getSize(128, 120); } }
        /// <summary>
        /// Gets the size128x128.
        /// </summary>
        public static GFXBoxSize Size128x128 { get { return getSize(128, 128); } }
        #endregion

        private static GFXBoxSize[,] sizes;

        /// <summary>
        /// Prevents a default instance of the <see cref="GFXBoxSize"/> class from being created.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        private GFXBoxSize(int width, int height) : base(width * height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// gets the size.
        /// </summary>
        /// <param name="Width">The width.</param>
        /// <param name="Height">The height.</param>
        /// <returns>A GFXBoxSize.</returns>
        internal static GFXBoxSize getSize(int Width, int Height)
        {
            if (sizes == null)
            {
                sizes = new GFXBoxSize[16, 16];
            }
            int w = (Width >> 3) - 1;
            int h = (Height >> 3) - 1;
            if (sizes[w, h] == null)
            {
                sizes[w, h] = new GFXBoxSize(Width, Height);
            }

            return sizes[w, h];
        }
    }
}
