using System;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;
using UnitsNet;

namespace WeatherDisplay
{
    public class TemperatureDiagram
    {
        public void Draw(
            SKCanvas canvas,
            int width, int height,
            TemperatureSet[] temperatureSets,
            float[] precipitation,
            Func<IEnumerable<TemperatureSet>, (Temperature Min, Temperature Max)> temperatureRangeSelector,
            DateTime now,
            TemperatureDiagramOptions options)
        {
            var topLeftX = (int)Math.Round(width * 0.08);
            var topLeftY = (int)Math.Round(height * 0.08);
            var bottomRightX = (int)Math.Round(width * 0.95);
            var bottomRightY = (int)Math.Round(height * 0.93);
            var chartArea = new SKRect(topLeftX, topLeftY, bottomRightX, bottomRightY);

            canvas.Clear(SKColors.White);

            var circleRadius = options.CircleRadius;
            var textPaint = options.TextPaint;

            var textPaintCenterAligned = textPaint.Clone();
            textPaintCenterAligned.TextAlign = SKTextAlign.Center;

            var textPaintRightAligned = textPaint.Clone();
            textPaintRightAligned.TextAlign = SKTextAlign.Right;

            var axisPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.LightGray,
                StrokeWidth = 1,
                StrokeCap = SKStrokeCap.Square,
            };

            var auxiliaryLinePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.LightGray,
                StrokeWidth = 1,
                StrokeCap = SKStrokeCap.Butt,
            };

            var currentDateTimeLinePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.LightGray,
                StrokeWidth = 4,
                StrokeCap = SKStrokeCap.Round,
                PathEffect = SKPathEffect.CreateDash(new[] { 0f, 8f }, 1),
                IsAntialias = true,
            };

            var temperatureLinePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColor.Parse("FF0317"),
                StrokeWidth = 5,
                StrokeCap = SKStrokeCap.Round,
                IsAntialias = true,
            };

            var temperatureMinMaxPaint = SKColor.Parse("#33FF0317");

            var precipitationLinePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.LightSteelBlue,
                StrokeWidth = 5,
                StrokeCap = SKStrokeCap.Round,
                IsAntialias = true,
            };

            var precipitationFillPaint = new SKPaint
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = SKColors.LightSteelBlue,
                StrokeWidth = 5,
                StrokeCap = SKStrokeCap.Round,
                IsAntialias = true,
            };

            var topLeft = new SKPoint(chartArea.Left, chartArea.Top);
            var topRight = new SKPoint(chartArea.Right, chartArea.Top);
            var bottomRight = new SKPoint(chartArea.Right, chartArea.Bottom);
            var bottomLeft = new SKPoint(chartArea.Left, chartArea.Bottom);

            // Draw X, Y and 2nd Y axis;
            canvas.DrawLine(bottomLeft, bottomRight, axisPaint);
            canvas.DrawLine(bottomLeft, new SKPoint(topLeft.X, topLeft.Y - 30), axisPaint);
            canvas.DrawLine(bottomRight, new SKPoint(topRight.X, topRight.Y - 30), axisPaint);

            // Draw hour text
            var xLabels = temperatureSets
                .GroupBy(t => t.DateTime.Date)
                .Select(i => $"{i.Key.ToShortDateString()}")
                .ToArray();

            var numberOfXLabels = xLabels.Length;
            var pixelsPerXLabel = chartArea.Width / numberOfXLabels;

            for (var i = 0; i < numberOfXLabels; i++)
            {
                var pointX = bottomLeft.X + (i * pixelsPerXLabel);

                var auxiliaryLineStartPoint = new SKPoint(pointX, topLeft.Y);
                var auxiliaryLineEndPoint = new SKPoint(pointX, bottomLeft.Y);
                canvas.DrawLine(auxiliaryLineStartPoint, auxiliaryLineEndPoint, auxiliaryLinePaint);

                var labelMarkerStartPoint = new SKPoint(pointX, bottomLeft.Y);
                var labelMarkerEndPoint = new SKPoint(pointX, bottomLeft.Y + 15);
                canvas.DrawLine(labelMarkerStartPoint, labelMarkerEndPoint, axisPaint);

                var labelText = xLabels[i];
                var labelTextWidth = textPaintCenterAligned.MeasureText(labelText);
                var labelTextPoint = new SKPoint(pointX + (pixelsPerXLabel / 2), bottomLeft.Y + 20);
                canvas.DrawText(labelText, labelTextPoint, textPaintCenterAligned);
            }

            // Calculate temperature value per pixel in Y axis
            var firstDateTime = temperatureSets.First().DateTime;
            var firstDate = firstDateTime.Date;
            var lastDateTime = temperatureSets.Last().DateTime;
            var lastDate = lastDateTime.Date.AddDays(1);
            var dateDiffFirstToLast = lastDate - firstDate;
            var pixelsPerTempX = chartArea.Width / (float)dateDiffFirstToLast.TotalHours;

            var tempBottomLeft = new SKPoint(chartArea.Left, chartArea.Bottom);

            var (Min, Max) = temperatureRangeSelector(temperatureSets);
            var absMinTemp = Min;
            var absMaxTemp = Max;
            var minMaxTempDiff = absMaxTemp - absMinTemp;
            var pixelsPerTempY = (float)(chartArea.Height / minMaxTempDiff.Value);

            // Draw horizontal auxiliary lines for temperature
            var numberOfAuxiliaryLines = 2;
            var pixelsPerAuxiliaryLine = chartArea.Height / numberOfAuxiliaryLines;

            for (var i = 0; i < numberOfAuxiliaryLines; i++)
            {
                var temperatureAuxiliaryLine = new Temperature(absMaxTemp.Value - (i * (minMaxTempDiff.Value / numberOfAuxiliaryLines)), absMinTemp.Unit);
                canvas.DrawLine(new SKPoint(bottomLeft.X, topLeft.Y + (i * pixelsPerAuxiliaryLine)), new SKPoint(topRight.X, topRight.Y + (i * pixelsPerAuxiliaryLine)), auxiliaryLinePaint);
                if (temperatureAuxiliaryLine.Value != absMaxTemp.Value && temperatureAuxiliaryLine.Value != absMinTemp.Value)
                {
                    canvas.DrawText(temperatureAuxiliaryLine.ToString("0"), bottomLeft.X - 20, topLeft.Y + 5 + (i * pixelsPerAuxiliaryLine), textPaintRightAligned);
                }
            }

            canvas.DrawLine(bottomLeft, new SKPoint(bottomLeft.X - 15, bottomLeft.Y), axisPaint);
            canvas.DrawLine(topLeft, new SKPoint(topLeft.X - 15, topLeft.Y), axisPaint);
            canvas.DrawText(absMinTemp.ToString("0"), bottomLeft.X - 20, bottomLeft.Y + 5, textPaintRightAligned);
            canvas.DrawText(absMaxTemp.ToString("0"), topLeft.X - 20, topLeft.Y + 5, textPaintRightAligned);

            var absMaxPrecipitation = precipitation.Max();
            var absMinPrecipitation = precipitation.Min();
            var minMaxPrecipitationDiff = absMaxPrecipitation - absMinPrecipitation;
            var pixelPerPrecipitation = (float)(chartArea.Height / minMaxPrecipitationDiff);

            // Draw labels for precipitation
            var numberOfAuxiliaryAxis = 4;
            for (var i = 0; i < numberOfAuxiliaryAxis; i++)
            {
                var guidelinePrecipitation = absMaxPrecipitation - (i * (minMaxPrecipitationDiff / numberOfAuxiliaryAxis));
                if (guidelinePrecipitation != absMaxPrecipitation && guidelinePrecipitation != absMaxPrecipitation)
                {
                    canvas.DrawText(guidelinePrecipitation.ToString("0.00"), bottomRight.X + 20, topLeft.Y + 5 + (i * pixelsPerAuxiliaryLine), textPaint);
                }
            }

            canvas.DrawLine(bottomRight, new SKPoint(bottomRight.X + 15, bottomRight.Y), axisPaint);
            canvas.DrawLine(topRight, new SKPoint(bottomRight.X + 15, topLeft.Y), axisPaint);
            canvas.DrawText(absMinPrecipitation.ToString("0.00"), bottomRight.X + 20, bottomLeft.Y + 5, textPaint);
            canvas.DrawText(absMaxPrecipitation.ToString("0.00"), bottomRight.X + 20, topLeft.Y + 5, textPaint);

            DrawCurrentDateLine(canvas, now, currentDateTimeLinePaint, topLeft, bottomLeft, firstDate, pixelsPerTempX);

            DrawTemperatureLine(canvas, temperatureSets, temperatureLinePaint, circleRadius, temperatureMinMaxPaint, tempBottomLeft, pixelsPerTempX, pixelsPerTempY, absMinTemp);

            DrawPrecipitationLine(canvas, precipitation, precipitationLinePaint, precipitationFillPaint, circleRadius, bottomLeft, pixelsPerTempX, absMinPrecipitation, pixelPerPrecipitation);

            //draw graph info
            var path3 = new SKPath();
            path3.MoveTo(bottomLeft.X + 100, topLeft.Y - 100);
            path3.LineTo(bottomLeft.X + 115, topLeft.Y - 100);
            canvas.DrawCircle(new SKPoint(bottomLeft.X + 115, topLeft.Y - 100), 7, temperatureLinePaint);
            path3.LineTo(bottomLeft.X + 130, topLeft.Y - 100);
            canvas.DrawPath(path3, temperatureLinePaint);
            canvas.DrawText("Temperature - °C (Left Axis)", new SKPoint(bottomLeft.X + 140, topLeft.Y - 95), textPaint);

            var path4 = new SKPath();
            path4.MoveTo(bottomLeft.X + 100, topLeft.Y - 50);
            path4.LineTo(bottomLeft.X + 115, topLeft.Y - 50);
            canvas.DrawCircle(new SKPoint(bottomLeft.X + 115, topLeft.Y - 50), 7, precipitationFillPaint);
            path4.LineTo(bottomLeft.X + 130, topLeft.Y - 50);
            canvas.DrawPath(path4, precipitationLinePaint);
            canvas.DrawText("Precipitation - mm (Right Axis)", new SKPoint(bottomLeft.X + 140, topLeft.Y - 45), textPaint);
        }

        private static void DrawCurrentDateLine(SKCanvas canvas, DateTime now, SKPaint currentDateTimeLinePaint, SKPoint topLeft, SKPoint bottomLeft, DateTime firstDate, float pixelsPerTempX)
        {
            var pixelsNowXOffset = CalculateXOffset(firstDate, now, pixelsPerTempX);

            var nowLineStartPointX = bottomLeft.X + pixelsNowXOffset;
            var nowLineStartPoint = new SKPoint(nowLineStartPointX, topLeft.Y);
            var nowLineEndPoint = new SKPoint(nowLineStartPointX, bottomLeft.Y);
            canvas.DrawLine(nowLineStartPoint, nowLineEndPoint, currentDateTimeLinePaint);
        }

        private static void DrawTemperatureLine(
            SKCanvas canvas,
            TemperatureSet[] temperatureSets,
            SKPaint temperatureLinePaint,
            float circleRadius,
            SKColor temperatureMinMaxPaint,
            SKPoint bottomLeft,
            float pixelsPerTempX,
            float pixelsPerTempY,
            Temperature minTemp)
        {
            var path = new SKPath();

            var firstDate = temperatureSets.First().DateTime.Date;
            var temp = temperatureSets[0];

            var startOffsetX = CalculateXOffset(firstDate, temp.DateTime, pixelsPerTempX);
            var startPointX = bottomLeft.X + startOffsetX;

            var startPointAvgY = bottomLeft.Y - ((float)(temp.Avg - minTemp).Value * pixelsPerTempY);
            var startPointAvg = new SKPoint(startPointX, startPointAvgY);

            var avgMinDiff = (float)(temp.Avg.Value - temp.Min.Value) * pixelsPerTempY;
            var avgMaxDiff = (float)(temp.Max.Value - temp.Avg.Value) * pixelsPerTempY;

            var startPointMin = new SKPoint(startPointX, startPointAvgY - avgMinDiff);
            var startPointMax = new SKPoint(startPointX, startPointAvgY + avgMaxDiff);

            path.MoveTo(startPointAvg);

            if (circleRadius > 0)
            {
                canvas.DrawCircle(startPointAvg, circleRadius, temperatureLinePaint);
            }

            for (var i = 1; i < temperatureSets.Length; i++)
            {
                var temperatureSet = temperatureSets[i];

                var nextOffsetX = CalculateXOffset(firstDate, temperatureSet.DateTime, pixelsPerTempX);
                var nextPointX = bottomLeft.X + nextOffsetX;
                var nextPointAvgY = bottomLeft.Y - ((float)(temperatureSet.Avg - minTemp).Value * pixelsPerTempY);
                var nextPointAvg = new SKPoint(nextPointX, nextPointAvgY);

                avgMinDiff = (float)(temperatureSet.Avg.Value - temperatureSet.Min.Value) * pixelsPerTempY;
                avgMaxDiff = (float)(temperatureSet.Max.Value - temperatureSet.Avg.Value) * pixelsPerTempY;

                var nextPointMin = new SKPoint(nextPointX, nextPointAvgY - avgMinDiff);
                var nextPointMax = new SKPoint(nextPointX, nextPointAvgY + avgMaxDiff);

                DrawMinMaxTemperatureShade(
                    canvas, temperatureMinMaxPaint,
                    (startPointMin, startPointAvg, startPointMax),
                    (nextPointMin, nextPointAvg, nextPointMax));

                if (circleRadius > 0)
                {
                    canvas.DrawCircle(nextPointAvg, circleRadius, temperatureLinePaint);
                }

                path.LineTo(nextPointAvg.X, nextPointAvg.Y);

                startPointAvg = nextPointAvg;
                startPointMin = nextPointMin;
                startPointMax = nextPointMax;
            }

            canvas.DrawPath(path, temperatureLinePaint);
        }

        private static float CalculateXOffset(DateTime firstDate, DateTime dateTime, float pixelsPerTempX)
        {
            var xoffset = (float)(dateTime - firstDate).TotalHours * pixelsPerTempX;
            return xoffset;
        }

        private static void DrawMinMaxTemperatureShade(
            SKCanvas canvas, SKColor color,
            (SKPoint Min, SKPoint Avg, SKPoint Max) start,
            (SKPoint Min, SKPoint Avg, SKPoint Max) end)
        {
            var paint = new SKPaint
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = color,
                IsAntialias = true,
            };

            var path = new SKPath
            {
                FillType = SKPathFillType.EvenOdd
            };

            path.MoveTo(start.Avg);
            path.LineTo(start.Max);
            path.LineTo(end.Max);
            path.LineTo(end.Avg);
            path.LineTo(end.Min);
            path.LineTo(start.Min);
            path.LineTo(start.Avg);
            path.Close();

            canvas.DrawPath(path, paint);
        }

        private static void DrawPrecipitationLine(SKCanvas canvas, float[] precipitation, SKPaint linePaint, SKPaint circlePaint, float circleRadius, SKPoint origin, float stepX, float minPrecipitation, float pixelPerPrecipitationMM)
        {
            var path = new SKPath();

            var startPointX = /*bottomLeft.X + (pixelsPerTempStep / 2);*/ 0; // TODO

            var startPointYPrecipitation = origin.Y - ((precipitation[0] - minPrecipitation) * pixelPerPrecipitationMM);
            var startPointPercipitation = new SKPoint(startPointX, startPointYPrecipitation);
            path.MoveTo(startPointPercipitation);

            if (circleRadius > 0)
            {
                canvas.DrawCircle(startPointPercipitation, circleRadius, circlePaint);
            }

            for (var i = 1; i < precipitation.Length; i++)
            {
                var nextPoint = new SKPoint(origin.X + (i * stepX) + (stepX / 2), origin.Y - ((precipitation[i] - minPrecipitation) * pixelPerPrecipitationMM));

                if (circleRadius > 0)
                {
                    canvas.DrawCircle(nextPoint, circleRadius, circlePaint);
                }

                path.LineTo(nextPoint.X, nextPoint.Y);
            }
            canvas.DrawPath(path, linePaint);
        }
    }
}