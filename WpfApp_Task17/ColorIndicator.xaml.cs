using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_Task17
{
    /// <summary>
    /// Логика взаимодействия для ColorIndicator.xaml
    /// </summary>
    public partial class ColorIndicator : UserControl
    {
        //Определяем свойства зависимости элемента управления
        public static readonly DependencyProperty ColorProperty;
        public static readonly DependencyProperty RedProperty;
        public static readonly DependencyProperty GreenProperty;
        public static readonly DependencyProperty BlueProperty;
        public static readonly RoutedEvent ColorChangedEvent;

        //Определяем обработчик события изменения цвета
        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add
            {
                AddHandler(ColorChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ColorChangedEvent, value);
            }
        }

        //Определяем свойства цвета и его составляющих
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
        public byte Red
        {
            get => (byte)GetValue(RedProperty);
            set => SetValue(RedProperty, value);
        }
        public byte Green
        {
            get => (byte)GetValue(GreenProperty);
            set => SetValue(GreenProperty, value);
        }
        public byte Blue
        {
            get => (byte)GetValue(BlueProperty);
            set => SetValue(BlueProperty, value);
        }

        //Инициализируем элемент управления
        public ColorIndicator()
        {
            InitializeComponent();
        }

        //Определяем статический конструктор, регистрирующий 4 свойства зависимости и 1 маршрутизируемое событие для индикатора цвета
        static ColorIndicator()
        {
            ColorProperty = DependencyProperty.Register(
                nameof(Color),
                typeof(Color),
                typeof(ColorIndicator),
                new FrameworkPropertyMetadata(
                    Colors.Black,
                    new PropertyChangedCallback(OnColorChanged)));
            RedProperty = DependencyProperty.Register(
                nameof(Red),
                typeof(byte),
                typeof(ColorIndicator),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            GreenProperty = DependencyProperty.Register(
                nameof(Green),
                typeof(byte),
                typeof(ColorIndicator),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            BlueProperty = DependencyProperty.Register(
                nameof(Blue),
                typeof(byte),
                typeof(ColorIndicator),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            ColorChangedEvent = EventManager.RegisterRoutedEvent(
                "ColorChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<Color>),
                typeof(ColorIndicator));
        }

        //Определяем метод, определяющий цвет при изменении одного из базовых цветов кодировки RGB
        private static void OnColorRGBChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorIndicator colorIndicator = (ColorIndicator)sender;
            Color color = colorIndicator.Color;
            if (e.Property == RedProperty)
                color.R = (byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;
            colorIndicator.Color = color;
        }

        //Определяем метод, устанавливающий свойства нового цвета
        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Color newcolor = (Color)e.NewValue;
            ColorIndicator colorIndicator = (ColorIndicator)d;
            colorIndicator.Red = newcolor.R;
            colorIndicator.Green = newcolor.G;
            colorIndicator.Blue = newcolor.B;
        }
    }
}
